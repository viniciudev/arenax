using Core.DTOs;
using Infrastructure.Authenticate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quadra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;
        public UserController( IUserService userService, IWebHostEnvironment environment )
        {
            _userService = userService;
            _environment = environment;
        }
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(UserAuthenticate userAuthenticate)
        {
            var token = await _userService.Auth(userAuthenticate);

            return Ok(token);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return  Ok(await _userService.GetUserById(id));
        }
        [HttpGet("profile")]
        [Authorize] // Adiciona autenticação JWT
        public async Task<IActionResult> Get()
        {
            // Extrai o UserId do token JWT
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId") ??
                             User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("Token inválido ou sem informação de usuário");
            }

            return Ok(await _userService.GetUserSportCenterById(userId));
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto value)
        {

            var result = await _userService.CreateUser(value);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Upload Imagem usuário
        /// </summary>
        /// <param name="dto.Id">Id do user</param>
        [HttpPost("UploadImage")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UploadImage([FromForm] UserImageDto dto)
        {
            if (dto.Image == null || dto.Image.Length == 0)
            {
                return BadRequest("Nenhum arquivo de imagem foi enviado.");
            }

            // Garante que a pasta de uploads existe
            var _uploadFolder = Path.Combine(_environment.ContentRootPath, "App_Data", "uploads");

            // Garantir que o diretório existe
            if (!Directory.Exists(_uploadFolder))
                Directory.CreateDirectory(_uploadFolder);
            // Gera um nome único para o arquivo
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;
            var filePath = Path.Combine(_uploadFolder, uniqueFileName);
            // Salva o arquivo
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(fileStream);
            }
            await _userService.UpdateUserImage(dto.Id, uniqueFileName);

            return Ok(new
            {
                Message = "Imagem recebida com sucesso",
                FileName = uniqueFileName
            });
        }
        /// <summary>
        /// busca uma imagem pelo UniqueFileName do usuário
        /// </summary>
        /// <param name="fileName">Identificador único da imagem</param>
        [HttpGet("GetByUniqueName/{fileName}")]
        public IActionResult GetByUniqueName(string fileName)
        {
            var uploadsFolder = Path.Combine(_environment.ContentRootPath, "App_Data", "uploads");
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound();
            // Mapeamento de extensões para MIME types
            var mimeTypes = new Dictionary<string, string>
                            {{".png", "image/png"},
                                {".jpg", "image/jpeg"},
                                {".jpeg", "image/jpeg"},
                                {".gif", "image/gif"},
                                {".bmp", "image/bmp"},
                                {".webp", "image/webp"},
                                {".svg", "image/svg+xml"}};
            // Obtém a extensão do arquivo
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            var mimeType = mimeTypes.TryGetValue(extension, out var type) ? type : "application/octet-stream";
            var imageFileStream = System.IO.File.OpenRead(filePath);
            return File(imageFileStream, mimeType); // Ajuste o MIME type conforme necessário
        }
        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        /// <param name="id">Identificador único do usuário</param>
        /// <param name="value">Novos dados do usuário</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDto value)
        {
            var resp=await _userService.UpdateUser(value);
            return Ok(resp);
        }

    }
}
