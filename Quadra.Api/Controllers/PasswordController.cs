using Core;
using Core.DTOs.PasswordRecover;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace Quadra.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;
        private readonly IOtpService _otpService;
        private readonly ILogger<PasswordController> _logger;

        public PasswordController(
            IPasswordService passwordService,
            IOtpService otpService,
            ILogger<PasswordController> logger)
        {
            _passwordService = passwordService;
            _otpService = otpService;
            _logger = logger;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Enviar OTP para reset de senha
                var result = await _otpService.GenerateAndSendOtpAsync(request.Email, "password_reset");

                if (result == null)
                {
                    return BadRequest(new
                    {
                        message = "Já existe um código ativo. Verifique seu email ou aguarde para solicitar um novo."
                    });
                }

                _logger.LogInformation("Solicitação de reset de senha para {Email}", request.Email);

                return Ok(new
                {
                    message = "Código de verificação enviado para seu email.",
                    expiresIn = 600 // 10 minutos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no forgot-password para {Email}", request.Email);
                return StatusCode(500, new { message = "Erro interno ao processar solicitação." });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Validação customizada da força da senha
                var strengthScore =await _passwordService.GetPasswordStrengthScore(request.NewPassword);
                if (strengthScore < 3)
                {
                    var errors = new List<string>();

                    if (request.NewPassword.Length < 8)
                        errors.Add("• Mínimo 8 caracteres");

                    if (!request.NewPassword.Any(char.IsUpper))
                        errors.Add("• Pelo menos 1 letra maiúscula");

                    if (!request.NewPassword.Any(char.IsLower))
                        errors.Add("• Pelo menos 1 letra minúscula");

                    if (!request.NewPassword.Any(char.IsDigit))
                        errors.Add("• Pelo menos 1 número");

                    if (!request.NewPassword.Any(ch => !char.IsLetterOrDigit(ch)))
                        errors.Add("• Pelo menos 1 caractere especial (@, #, $, etc.)");

                    return BadRequest(new
                    {
                        message = "Senha fraca. Requisitos:",
                        errors,
                        strengthScore
                    });
                }
                // Resetar senha usando OTP
                var result = await _passwordService.ResetPasswordWithOtpAsync(
                    request.Email,
                    request.OtpCode,
                    request.NewPassword
                );

                if (!result)
                {
                    return BadRequest(new
                    {
                        message = "Não foi possível resetar a senha. Verifique o código ou tente novamente."
                    });
                }

                _logger.LogInformation("Senha resetada com sucesso para {Email}", request.Email);

                return Ok(new { message = "Senha redefinida com sucesso." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no reset-password para {Email}", request.Email);
                return StatusCode(500, new { message = "Erro interno ao redefinir senha." });
            }
        }

        //[HttpPost("change-password")]
        //[Authorize] // Requer autenticação
        //public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        // Obter email do usuário autenticado
        //        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        //        if (string.IsNullOrEmpty(userEmail))
        //        {
        //            return Unauthorized();
        //        }

        //        var result = await _passwordService.ChangePasswordAsync(
        //            userEmail,
        //            request.CurrentPassword,
        //            request.NewPassword
        //        );

        //        if (!result)
        //        {
        //            return BadRequest(new
        //            {
        //                message = "Senha atual incorreta ou usuário não encontrado."
        //            });
        //        }

        //        _logger.LogInformation("Senha alterada com sucesso para {Email}", userEmail);

        //        return Ok(new { message = "Senha alterada com sucesso." });
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro no change-password para usuário autenticado");
        //        return StatusCode(500, new { message = "Erro interno ao alterar senha." });
        //    }
        //}

        //[HttpPost("validate-strength")]
        //public async Task< IActionResult> ValidatePasswordStrength([FromBody] PasswordStrengthRequest request)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(request.Password))
        //        {
        //            return BadRequest(new { message = "Senha não fornecida." });
        //        }

        //        var strengthScore = await _passwordService.GetPasswordStrengthScore(request.Password);
        //        var isValid = strengthScore >= 3; // Mínimo 3 pontos para ser válida

        //        return Ok(new
        //        {
        //            isValid,
        //            score = strengthScore,
        //            message = isValid ? "Senha forte" : "Senha fraca"
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao validar força da senha");
        //        return StatusCode(500, new { message = "Erro interno ao validar senha." });
        //    }
        //}
    }

    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "⚠️ O email é obrigatório")]
        [EmailAddress(ErrorMessage = "⚠️ Email em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "⚠️ O código de verificação é obrigatório")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "⚠️ O código deve ter exatamente 6 dígitos")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "⚠️ O código deve conter apenas números")]
        public string OtpCode { get; set; }

        [Required(ErrorMessage = "⚠️ A nova senha é obrigatória")]
        [MinLength(8, ErrorMessage = "⚠️ A senha deve ter no mínimo 8 caracteres")]
        [MaxLength(100, ErrorMessage = "⚠️ A senha deve ter no máximo 100 caracteres")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "⚠️ A confirmação de senha é obrigatória")]
        [Compare("NewPassword", ErrorMessage = "⚠️ As senhas não coincidem")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "A nova senha deve ter pelo menos 6 caracteres")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "As senhas não coincidem")]
        public string ConfirmPassword { get; set; }
    }

    public class PasswordStrengthRequest
    {
        [Required]
        public string Password { get; set; }
    }
}
