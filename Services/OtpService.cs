using Core;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public interface IOtpService
    {
        Task<string> GenerateAndSendOtpAsync(string email, string purpose);
        Task<bool> ValidateOtpAsync(string email, string code, string purpose);
        Task<bool> RevokeOtpAsync(string email, string purpose);
    }

    public class OtpService : IOtpService
    {
        private readonly IUnitOfWork _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public OtpService(IUnitOfWork context, IEmailService emailService, IConfiguration configuration)
        {
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<string> GenerateAndSendOtpAsync(string email, string purpose)
        {
            // Verificar se já existe um OTP ativo
            var existingOtp = await _context.Otp
                .GetByFilters(email,purpose);

            if (existingOtp != null)
            {
                // Se já existe um OTP válido, retornar null para evitar spam
                return null;
            }

            // Gerar código de 6 dígitos
            var code = GenerateSixDigitCode();

            // Hash do código para armazenamento seguro
            var hashedCode = HashCode(code);

            // Criar OTP
            var otp = new Otp
            {
                Email = email,
                HashedCode = hashedCode,
                Purpose = purpose,
                ExpiryDate = DateTime.UtcNow.AddMinutes(10), // 10 minutos de validade
                CreatedAt = DateTime.UtcNow,
                Attempts = 0
            };

           await  _context.Otp.Add(otp);
             _context.Save();

            // Enviar email com o código
            await SendOtpEmail(email, code, purpose);

            return code;
        }

        public async Task<bool> ValidateOtpAsync(string email, string code, string purpose)
        {
            var otp = await _context.Otp
                .GetByFilters(email, purpose);

            if (otp == null)
                return false;

            // Incrementar tentativas
            otp.Attempts++;

            // Verificar se excedeu o limite de tentativas
            if (otp.Attempts >= 5)
            {
                _context.Otp.Delete(otp);
             _context.Save();
                return false;
            }

            // Verificar se o código corresponde
            var isValid = VerifyCode(code, otp.HashedCode);

            if (isValid)
            {
                // Remover OTP após uso bem-sucedido
                _context.Otp.Delete(otp);
            }

            _context.Save();

            return isValid;
        }

        public async Task<bool> RevokeOtpAsync(string email, string purpose)
        {
            var otps = await _context.Otp
               .GetByEmailPurpose(email, purpose);

            if (otps.Any())
            {
                foreach (var item in otps)
                {
                    _context.Otp.Delete(item);
                    _context.Save();
                }
               
            }
            return true;
        }

        private string GenerateSixDigitCode()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            var number = BitConverter.ToUInt32(bytes, 0) % 1000000;
            return number.ToString("D6");
        }

        private string HashCode(string code)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(code + _configuration["Otp:Salt"]));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyCode(string code, string hashedCode)
        {
            var hashedInput = HashCode(code);
            return hashedInput == hashedCode;
        }

        private async Task SendOtpEmail(string email, string code, string purpose)
        {
            var subject = purpose switch
            {
                "password_reset" => "Código de Recuperação de Senha - ArenaX",
                "email_verification" => "Verificação de Email - ArenaX",
                "2fa" => "Autenticação Two-Factor - ArenaX",
                _ => "Seu Código de Verificação - ArenaX"
            };

            var message = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                    <h2 style='color: #2563eb;'>ArenaX App</h2>
                    <h3>{subject}</h3>
                    <p>Seu código de verificação é:</p>
                    <div style='background-color: #f3f4f6; padding: 20px; text-align: center; margin: 20px 0;'>
                        <span style='font-size: 32px; font-weight: bold; letter-spacing: 5px; color: #2563eb;'>{code}</span>
                    </div>
                    <p>Este código é válido por <strong>10 minutos</strong>.</p>
                    <p>Se você não solicitou este código, ignore este email.</p>
                    <hr style='border: none; border-top: 1px solid #e5e7eb; margin: 20px 0;'>
                    <p style='color: #6b7280; font-size: 12px;'>Este é um email automático, por favor não responda.</p>
                </div>";

            await _emailService.SendEmailAsync(email, subject, message);
        }
    }
}
