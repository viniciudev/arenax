using Microsoft.AspNetCore.Mvc;
using Services;
using System.ComponentModel.DataAnnotations;

namespace Quadra.Api.Controllers
{
   
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IOtpService _otpService;
        private readonly ILogger<OtpController> _logger;

        public OtpController(IOtpService otpService, ILogger<OtpController> logger)
        {
            _otpService = otpService;
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var code = await _otpService.GenerateAndSendOtpAsync(request.Email, request.Purpose);

                if (code == null)
                {
                    return BadRequest(new
                    {
                        message = "Já existe um código ativo. Aguarde para solicitar um novo."
                    });
                }

                _logger.LogInformation("OTP enviado para {Email} - Propósito: {Purpose}", request.Email, request.Purpose);

                return Ok(new
                {
                    message = "Código enviado com sucesso.",
                    expiresIn = 600 // 10 minutos em segundos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar OTP para {Email}", request.Email);
                return StatusCode(500, new { message = "Erro interno ao enviar código." });
            }
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateOtp([FromBody] ValidateOtpRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var isValid = await _otpService.ValidateOtpAsync(request.Email, request.Code, request.Purpose);

                if (!isValid)
                {
                    _logger.LogWarning("Tentativa de validação falhou para {Email} - Código: {Code}",
                        request.Email, request.Code);

                    return BadRequest(new
                    {
                        message = "Código inválido, expirado ou excedeu o número de tentativas."
                    });
                }

                _logger.LogInformation("OTP validado com sucesso para {Email}", request.Email);

                return Ok(new
                {
                    message = "Código validado com sucesso.",
                    valid = true
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao validar OTP para {Email}", request.Email);
                return StatusCode(500, new { message = "Erro interno ao validar código." });
            }
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeOtp([FromBody] RevokeOtpRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _otpService.RevokeOtpAsync(request.Email, request.Purpose);

                _logger.LogInformation("OTP revogado para {Email} - Propósito: {Purpose}",
                    request.Email, request.Purpose);

                return Ok(new { message = "Códigos revogados com sucesso." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao revogar OTP para {Email}", request.Email);
                return StatusCode(500, new { message = "Erro interno ao revogar códigos." });
            }
        }
    }
    public class SendOtpRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Purpose { get; set; } // password_reset, email_verification, 2fa
    }

    public class ValidateOtpRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "O código deve ter 6 dígitos")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "O código deve conter apenas números")]
        public string Code { get; set; }

        [Required]
        public string Purpose { get; set; }
    }

    public class RevokeOtpRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Purpose { get; set; }
    }
}
