using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Quadra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportCenterController : ControllerBase
    {
        private readonly ISportsCenterService _sportsCenterService;
        private readonly IWebHostEnvironment _environment;
        public SportCenterController(ISportsCenterService sportsCenterService, IWebHostEnvironment environment)
        {
            _sportsCenterService = sportsCenterService;
            _environment = environment;
        }
        /// <summary>
        /// Obtém centro esportivos por filtro
        /// </summary>

        /// <returns>Detalhes do centro esportiva</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] SportCenterFilter value)
        {
            var resp = await _sportsCenterService.GetAllByFilter(value);
            return Ok(resp);
            //return Ok(resp);
        }
        /// <summary>
        /// Obtém um centro esportivo específico pelo ID
        /// </summary>
        /// <param name="id">Identificador único do centro</param>
        /// <returns>Detalhes do centro esportiva</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resp = await _sportsCenterService.GetSportsCenterById(id);
            return Ok(resp);
            //return Ok(resp);
        }
        /// <summary>
        /// Upload logo centro esportivo
        /// </summary>
        /// <param name="value">Dados logo a ser criado</param>
        [HttpPost("UploadLogo")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UploadLogo([FromForm] SportsCenterLogoDto dto)
        {

            if (dto.Logo == null)
            {
                return BadRequest("Nenhum arquivo de imagem foi enviado.");
            }

            // Garante que a pasta de uploads existe
            var _uploadFolder = Path.Combine(_environment.ContentRootPath, "App_Data", "uploads");

            // Garantir que o diretório existe
            if (!Directory.Exists(_uploadFolder))
                Directory.CreateDirectory(_uploadFolder);
            var uniqueFileName = $"{Guid.NewGuid()}_{dto.Logo.FileName}";
            var filePath = Path.Combine(_uploadFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Logo.CopyToAsync(fileStream);
            }

            // Chame seu serviço para atualizar o centro esportivo com o logo
            await _sportsCenterService.UpdateSportsCenterLogo(dto.Id, uniqueFileName);

            return Ok(new
            {
                Message = "Logo recebido com sucesso",
                //SizeInBytes = logoBytes.Length,
                FileName = dto.Logo.FileName
            });
        }
        /// <summary>
        /// busca uma imagem pelo UniqueFileName do centro esportivo
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
        /// Cria um novo centro esportivo
        /// </summary>
        /// <param name="value">Dados do centro a ser criado</param>
        [HttpPost]

        public async Task<ActionResult> Post([FromBody] SportsCenterDto value)
        {
            return Ok(await _sportsCenterService.CreateSportsCenter(value));
        }
        /// <summary>
        /// Atualiza um centro esportivo existente
        /// </summary>

        /// <param name="value">Novos dados do centro</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] SportsCenterDto value)
        {
            await _sportsCenterService.UpdateSportsCenter(value);
            return Ok(true);
        }
    }
}
