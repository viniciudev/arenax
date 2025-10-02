using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quadra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsRegistrationController : ControllerBase
    {
        private readonly ISportsRegistrationService _SportsRegistrationService;
        public SportsRegistrationController(ISportsRegistrationService SportsRegistrationService)
        {
            _SportsRegistrationService = SportsRegistrationService;
        }

        /// <summary>
        /// Obtém todos as incrições disponíveis por id do agendamento
        /// </summary>
        /// /// <param name="id">Identificador único do agendamento</param>
        /// <returns>Lista de inscrições</returns>

        [HttpGet("GetAllById")]
        public async Task<ActionResult> GetAllByIdSportsCenter([FromQuery] int id)
        {
            return Ok(await _SportsRegistrationService.GetAllSportsRegistrations(id));
        }

        /// <summary>
        /// Obtém todos SportsRegistration específico pelo ID do cliente
        /// </summary>
        /// <param name="id">Identificador único do Cliente</param>
        /// <returns>Detalhes</returns>
        [HttpGet("GetAllByIdClient/{id}")]
        public async Task<ActionResult> GetAllByIdClient(int id)
        {
            return Ok(await _SportsRegistrationService.GetAllByIdClient(id));
        }
        /// <summary>
        /// Cria uma nova Inscrição
        /// </summary>
        /// <param name="value">Dados da inscrição a ser criado</param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SportsRegistrationDto value)
        {
            return Ok(await _SportsRegistrationService.CreateSportsRegistrations(value));
        }


        /// <summary>
        /// Atualiza uma inscrição e seu status existente
        /// </summary>
        /// <param name="value">Novos dados</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] SportsRegistrationDto value)
        {
            await _SportsRegistrationService.UpdateSportsRegistrations(value);
            return Ok(true);
        }
        /// <summary>
        /// Excluir um SportsRegistratione existente
        /// </summary>
        /// <param name="id">Identificador único do SportsRegistratione</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _SportsRegistrationService.DeleteSportsRegistrations(id);
            return Ok(true);
        }
    }
}
