using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Quadra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsService _NotificationsService;
        public NotificationsController(INotificationsService NotificationsService)
        {
            _NotificationsService = NotificationsService;
        }

        /// <summary>
        /// Obtém todas notificações de um cliente específico pelo ID
        /// </summary>
        /// <param name="id">Identificador único do cliente</param>
        /// <returns>Detalhes das comodidades</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resp = await _NotificationsService.GetAllNotificationsByIdClient(id);
            return Ok(resp);
        }

        /// <summary>
        /// Cria uma nova notificação do cliente
        /// </summary>
        /// <param name="value">Dados da notificação a ser criada</param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NotificationsDto value)
        {
            return Ok(await _NotificationsService.CreateNotifications(value));
        }
        /// <summary>
        /// Atualiza uma notificação
        /// </summary>
        /// <param name="value">Novos dados da notificação</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] NotificationsDto value)
        {
            await _NotificationsService.UpdateNotifications(value);
            return Ok(true);
        }
        /// <summary>
        /// Deleta uma notificação existente
        /// </summary>
        /// <param name="id">Identificador único da notificação</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _NotificationsService.DeleteNotifications(id);
            return Ok(true);
        }
    }
}
