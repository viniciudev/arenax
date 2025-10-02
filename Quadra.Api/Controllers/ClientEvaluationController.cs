using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Quadra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientEvaluationController : ControllerBase
    {
        private readonly IClientEvaluationService _clientEvaluationService;
        public ClientEvaluationController(IClientEvaluationService clientEvaluationService)
        {
            _clientEvaluationService = clientEvaluationService;
        }
        /// <summary>
        /// Obtém um cliente específica pelo telefone
        /// </summary>
        /// <param name="phone">Identificador único do cliente</param>
        /// <returns>Detalhes</returns>
        //[HttpGet("GetByPhone/{phone}")]
        //public async Task<ActionResult> GetByPhone(string phone)
        //{
        //    ResponseClient? client = await _clientService.GetClientsByPhone(phone);
        //    if (client == null)
        //    {
        //        return BadRequest("Não foi localizado nenhum cliente!");
        //    }
        //    return Ok(client);
        //}
        /// <summary>
        /// Obtém as avaliações de cliente pelo ID
        /// </summary>
        /// <param name="id">Identificador único do cliente</param>
        /// <returns>Detalhes</returns>

        [HttpGet("GetAll/{id}")]
        public async Task<ActionResult> GetAllByIdClient(int id)
        {
            return Ok(await _clientEvaluationService.GetAllClientEvaluationsByIdClient(id));
        }

        /// <summary>
        /// Obtém uma avaliação de cliente específica pelo ID
        /// </summary>
        /// <param name="id">Identificador único da avaliação</param>
        /// <returns>Detalhes</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _clientEvaluationService.GetClientEvaluationsById(id));
        }
        /// <summary>
        /// Cria uma nova avaliação do cliente
        /// </summary>
        /// <param name="value">Dados da avaliação cliente a ser criado</param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClientEvaluationDto value)
        {
            return Ok(await _clientEvaluationService.CreateClientEvaluations(value));
        }

        /// <summary>
        /// Atualiza uma avaliação de cliente existente
        /// </summary>
        /// <param name="value">Novos dados</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ClientEvaluationDto value)
        {
            await _clientEvaluationService.UpdateClientEvaluations(value);
            return Ok(true);
        }
        /// <summary>
        /// Excluir uma avaliação de cliente existente
        /// </summary>
        /// <param name="id">Identificador único da avaliação</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _clientEvaluationService.DeleteClientEvaluations(id);
            return Ok(true);
        }
    }
}
