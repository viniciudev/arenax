using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Quadra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsCenterController : ControllerBase
    {
        private readonly ISportsCenterService _sportsCenterService;
        public SportsCenterController(ISportsCenterService sportsCategoryService)
        {
            _sportsCenterService = sportsCategoryService;
        }
      
        /// <summary>
        /// Obtém um centro esportivo específico pelo ID
        /// </summary>
        /// <param name="id">Identificador único do centro</param>
        /// <returns>Detalhes do centro esportiva</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var resp = await _sportsCenterService.GetSportsCenterById(id);
            return Ok(resp);
        }
        /// <summary>
        /// Upload logo centro esportivo
        /// </summary>
        /// <param name="value">Dados logo a ser criado</param>
        [HttpPost("UploadLogo")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UploadLogo([FromForm] SportsCenterLogoDto dto)
        {
            if (dto.Logo == null || dto.Logo.Length == 0)
            {
                return BadRequest("Nenhum arquivo de logo foi enviado.");
            }
            // Converter o arquivo para array de bytes
            byte[] logoBytes;
            using (var memoryStream = new MemoryStream())
            {
                await dto.Logo.CopyToAsync(memoryStream);
                logoBytes = memoryStream.ToArray();
            }
            // Chame seu serviço para atualizar o centro esportivo com o logo
            await _sportsCenterService.UpdateSportsCenterLogo(dto.Id, logoBytes);

            return Ok(new
            {
                Message = "Logo recebido com sucesso",
                SizeInBytes = logoBytes.Length,
                FileName = dto.Logo.FileName
            });
        }
        /// <summary>
        /// Cria um novo centro esportivo
        /// </summary>
        /// <param name="value">Dados do centro a ser criado</param>
        [HttpPost]

        public async Task<ActionResult> Post([FromBody] SportsCenterDto value)
        {
            await _sportsCenterService.CreateSportsCenter(value);
            return Ok(true);
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
