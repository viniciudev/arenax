using Core;
using Infrastructure;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPasswordService
    {
        Task<bool> ResetPasswordWithOtpAsync(string email, string otpCode, string newPassword);
        Task<bool> ValidatePasswordAsync(string email, string password);
        Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword);
        Task<int> GetPasswordStrengthScore(string password);
    }
    public class PasswordService : IPasswordService
    {
        private readonly DbContextClass _context;
        private readonly IOtpService _otpService;
        private readonly IConfiguration _configuration;

        public PasswordService(DbContextClass context, IOtpService otpService, IConfiguration configuration)
        {
            _context = context;
            _otpService = otpService;
            _configuration = configuration;
        }

        public async Task<bool> ResetPasswordWithOtpAsync(string email, string otpCode, string newPassword)
        {
            // Validar OTP primeiro
            var isOtpValid = await _otpService.ValidateOtpAsync(email, otpCode, "password_reset");
            if (!isOtpValid)
            {
                return false;
            }

            // Buscar usuário
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return false;
            }

            // Verificar força da senha
            var strengthScore = await GetPasswordStrengthScore(newPassword);
            if (strengthScore < 3) // Mínimo de segurança
            {
                throw new ArgumentException("A senha não atende aos requisitos de segurança");
            }

            // Hash da nova senha
            user.Password = HashPassword(newPassword);

            // Limpar tokens de reset anteriores
            //user.PasswordResetToken = null;
            //user.PasswordResetTokenExpiry = null;

            await _context.SaveChangesAsync();

            // Revogar todos os OTPs de password reset para este email
            await _otpService.RevokeOtpAsync(email, "password_reset");

            return true;
        }

        public async Task<bool> ValidatePasswordAsync(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;

            return VerifyPassword(password, user.Password);
        }

        public async Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;

            // Verificar senha atual
            if (!VerifyPassword(currentPassword, user.Password))
                return false;

            // Verificar força da nova senha
            var strengthScore = await GetPasswordStrengthScore(newPassword);
            if (strengthScore < 3)
            {
                throw new ArgumentException("A nova senha não atende aos requisitos de segurança");
            }

            // Atualizar senha
            user.Password = HashPassword(newPassword);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetPasswordStrengthScore(string password)
        {
            if (string.IsNullOrEmpty(password))
                return  0;

            int score = 0;

            // Comprimento mínimo
            if (password.Length >= 8) score++;
            if (password.Length >= 12) score++;

            // Diversidade de caracteres
            if (password.Any(char.IsUpper)) score++;
            if (password.Any(char.IsLower)) score++;
            if (password.Any(char.IsDigit)) score++;
            if (password.Any(ch => !char.IsLetterOrDigit(ch))) score++;

            // Pontuação extra para senhas longas
            if (password.Length >= 16) score++;

            return Math.Min(score, 5); // Máximo 5 pontos
        }

        private string HashPassword(string password)
        {
            // Usando BCrypt para hash seguro
            return Encrypt.EncryptPassword(password); // Work factor 12
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return Encrypt.EncryptPassword(password)== hashedPassword;
        }
    }
}
    

