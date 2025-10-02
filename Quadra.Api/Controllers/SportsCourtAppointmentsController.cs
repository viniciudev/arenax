using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Quadra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsCourtAppointmentsController : ControllerBase
    {
        private readonly ISportsCourtAppointmentsService _sportsCourtAppointmentsService;
        public SportsCourtAppointmentsController(ISportsCourtAppointmentsService sportsCourtAppointmentsService)
        {
            _sportsCourtAppointmentsService = sportsCourtAppointmentsService;
        }
        /// <summary>
        /// Obtém todas as agendas ligadas a quadra
        /// </summary>
        /// <returns>Lista de agendas esportivas</returns>
        /// <param name="id">Identificador único da quadra</param>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllSportsCourtAppointments([FromQuery] ScheduleFilter scheduleFilter)
        {
            return Ok(await _sportsCourtAppointmentsService.GetAllSportsCourtAppointments(scheduleFilter));
        }

        /// <summary>
        /// Obtém uma agenda específica pelo ID
        /// </summary>
        /// <param name="id">Identificador único da agenda</param>
        /// <returns>Detalhes da agenda</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _sportsCourtAppointmentsService.GetSportsCourtAppointmentsById(id));
        }
        /// <summary>
        /// Cria uma nova agenda
        /// </summary>
        /// <param name="value">Dados da agenda a ser criada</param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SportsCourtAppointmentsDto value)
        {
            await _sportsCourtAppointmentsService.CreateSportsCourtAppointments(value);
            return Ok(true);
        }
        /// <summary>
        /// Cria lista de novas agendas
        /// </summary>
        /// <param name="value">Dados da agenda a ser criada</param>
        [HttpPost("List")]
        public async Task<ActionResult> PostList([FromBody] List<SportsCourtAppointmentsDto> value)
        {
            await _sportsCourtAppointmentsService.CreateSportsCourtAppointmentsList(value);
            return Ok(true);
        }

        /// <summary>
        /// Atualiza uma agenda existente
        /// </summary>
        /// <param name="id">Identificador único da agenda</param>
        /// <param name="value">Novos dados da agenda</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] SportsCourtAppointmentsDto value)
        {
            await _sportsCourtAppointmentsService.UpdateSportsCourtAppointments(value);
            return Ok(true);
        }
        /// <summary>
        /// Obtém todos os horarios disponíveis  agrupados por quadra
        /// </summary>
        /// <returns>Lista de horários agrupados</returns>
        /// <param name="TenantID">Identificador único do centro esportivo</param>
        [HttpGet("GetSchedulesCourt")]
        public async Task<ActionResult> GetSchedulesCourt(
         [FromQuery] RequestSchedulingsCourt requestSchedulingsCourt, [FromHeader] int TenantID)
        {
            return Ok(await _sportsCourtAppointmentsService
                .GetSchedulesCourt(requestSchedulingsCourt, TenantID));
        }
        /// <summary>
        /// Cancelar o agendamento alterando seu status
        /// </summary>
        /// <param name="id">Identificador único do agendamento</param>
        [HttpPut("cancelAppointment/{id}")]
        public async Task<ActionResult> CancelAppointment( int id)
        {
            return Ok(await _sportsCourtAppointmentsService
                .CancelAppointment(id));
        }
        /// <summary>
        /// Autoriza o agendamento alterando seu status
        /// </summary>
        /// <param name="id">Identificador único do agendamento</param>
        [HttpPut("acceptAppointment/{id}")]
        public async Task<ActionResult> AcceptAppointment(int id)
        {
            return Ok(await _sportsCourtAppointmentsService
                .AcceptAppointment(id));
        }
    }
}
