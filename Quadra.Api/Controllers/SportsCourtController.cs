using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quadra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SportsCourtController : ControllerBase
    {
        private readonly ISportsCourtService _sportsCourtService;
        private readonly IWebHostEnvironment _environment;
        public SportsCourtController(ISportsCourtService sportsCourtService, IWebHostEnvironment environment)
        {
            _sportsCourtService = sportsCourtService;
            _environment = environment;
        }
        /// <summary>
        /// Obtém todas as quadras esportivas disponíveis
        /// </summary>
        /// <returns>Lista de quadras esportivas</returns>
        ///   <param name="TenantID">ID do centro esportivo</param>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll([FromHeader] int TenantID)
        {
            return Ok(await _sportsCourtService.getall(TenantID));
        }

        /// <summary>
        /// Obtém uma quadra esportiva específica pelo ID
        /// </summary>
        /// <param name="id">Identificador único da quadra</param>
        /// <returns>Detalhes da quadra esportiva</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var resp = await _sportsCourtService.GetSportsCourtById(id);
            return Ok(resp);
        }
        /// <summary>
        /// Cria uma nova quadra esportiva
        /// </summary>
        /// <param name="value">Dados da quadra a ser criada</param>
        ///  <param name="TenantID">ID do centro esportivo</param>

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SportCourtDto value, [FromHeader] int TenantID)
        {
            value.IdSportsCenter = TenantID;
            
            return Ok(await _sportsCourtService.CreateSportsCourt(value));
        }

        /// <summary>
        /// Atualiza uma quadra esportiva existente
        /// </summary>
        /// <param name="id">Identificador único da quadra</param>
        /// <param name="value">Novos dados da quadra</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] SportCourtDto value)
        {
            await _sportsCourtService.UpdateSportsCourt(value);
            return Ok(true);
        }
        /// <summary>
        /// Uploads Imagens quadra esportiva
        /// </summary>
        /// <param name="dto.Id">Id da quadra</param>
        [HttpPost("UploadImages")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UploadImage([FromForm] SportsCourtImageDto dto)
        {
            if (dto.Images == null || !dto.Images.Any())
            {
                return BadRequest("Nenhum arquivo de imagem foi enviado.");
            }
           
            // Garante que a pasta de uploads existe
            var _uploadFolder = Path.Combine(_environment.ContentRootPath, "App_Data", "uploads");

            // Garantir que o diretório existe
            if (!Directory.Exists(_uploadFolder))
                Directory.CreateDirectory(_uploadFolder);
            var savedFiles = new List<string>();

            foreach (var image in dto.Images)
            {
                if (image.Length > 0)
                {
                    var uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
                    var filePath = Path.Combine(_uploadFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    savedFiles.Add(uniqueFileName);

                }
            }

            // Atualiza no serviço (se for para salvar vários)
            await _sportsCourtService.UpdateSportsCourtImage(dto.Id, savedFiles);
            return Ok(new
            {
                Message = "Imagens recebidas com sucesso",
                Files = savedFiles
            });
        }
        /// <summary>
        /// busca uma imagem pelo UniqueFileName da quadra esportiva existente
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
        /// Deleta uma quadra esportiva existente
        /// </summary>
        /// <param name="id">Identificador único da quadra</param>

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _sportsCourtService.DeleteSportsCourt(id);
            return Ok(true);
        }
        /// <summary>
        /// Deleta uma imagem da pasta de uploads
        /// </summary>
        /// <param name="uniqueFileName">Nome do arquivo a ser deletado</param>

        [HttpDelete("DeleteImage/{uniqueFileName}")]
        public async Task<IActionResult> DeleteImage(string uniqueFileName)
        {
            if (string.IsNullOrEmpty(uniqueFileName))
            {
                return BadRequest("Nome do arquivo não foi fornecido.");
            }

            var _uploadFolder = Path.Combine(_environment.ContentRootPath, "App_Data", "uploads");
            var filePath = Path.Combine(_uploadFolder, uniqueFileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Arquivo não encontrado.");
            }

            try
            {
                await _sportsCourtService.DeleteSportsCourtImage(uniqueFileName);
                System.IO.File.Delete(filePath);
            
                return Ok(new { Message = $"Arquivo {uniqueFileName} deletado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao deletar o arquivo: {ex.Message}" });
            }
        }
    }
}
