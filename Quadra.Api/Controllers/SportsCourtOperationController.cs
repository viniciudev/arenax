using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quadra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsCourtOperationController : ControllerBase
    {
        private readonly ISportsCourtOperationService _sportsCourtOperationService;
        public SportsCourtOperationController(ISportsCourtOperationService sportsCourtOperationService)
        {
            _sportsCourtOperationService = sportsCourtOperationService;
        }
        /// <summary>
        /// Obtém horários de funcionamento ligadas a quadra
        /// </summary>
        /// <returns>Lista de horários de funcionamento da quadra</returns>
        /// <param name="id">Identificador único da quadra</param>
        [HttpGet("GetAll/{id}")]
        public async Task<ActionResult> GetAllSportsCourtOperationByIdCourt(int id)
        {
            return Ok(await _sportsCourtOperationService.GetAllSportsCourtOperationByIdCourt(id));
        }

        /// <summary>
        /// Obtém um horário de funcionamento pelo ID
        /// </summary>
        /// <param name="id">Identificador horário de funcionamento</param>
        /// <returns>Detalhes horário de funcionamento</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _sportsCourtOperationService.GetSportsCourtOperationById(id));
        }
        /// <summary>
        /// Cria um horário de funcionamento
        /// </summary>
        /// <param name="value">Dados horário de funcionamento a ser criado</param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SportsCourtOperationDto value)
        {
            await _sportsCourtOperationService.CreateSportsCourtOperation(value);
            return Ok(true);
        }

        /// <summary>
        /// Atualiza um horário de funcionamento existente
        /// </summary>
        /// <param name="id">Identificador único do horário de funcionamento</param>
        /// <param name="value">Novos dados  horário de funcionamento</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] SportsCourtOperationDto value)
        {
            await _sportsCourtOperationService.UpdateSportsCourtOperation(value);
            return Ok(true);
        }
        /// <summary>
        /// Deleta um horário de funcionamento existente
        /// </summary>
        /// <param name="id">Identificador único do horário de funcionamento</param>
        /// <param name="value">Novos dados  horário de funcionamento</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _sportsCourtOperationService.DeleteSportsCourtOperation(id);
            return Ok(true);
        }
    }
}
