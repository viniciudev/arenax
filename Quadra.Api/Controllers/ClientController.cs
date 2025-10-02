using Core;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quadra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
       private readonly IClientService _clientService;
        public ClientController(IClientService clientService) {
        _clientService = clientService;
        }
        /// <summary>
        /// Obtém um cliente específica pelo telefone
        /// </summary>
        /// <param name="phone">Identificador único do cliente</param>
        /// <returns>Detalhes</returns>
        [HttpGet("GetByPhone/{phone}")]
        public async Task<ActionResult> GetByPhone(string phone)
        {
            ResponseClient? client = await _clientService.GetClientsByPhone(phone);
            if (client == null)
            {
                return BadRequest("Não foi localizado nenhum cliente!");
            }
            return Ok(client);
        }
        /// <summary>
        /// Obtém todos os clientes
        /// </summary>
        /// <returns>Lista de clientes</returns>
        
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllByIdSportsCenter()
        {
            return Ok(await _clientService.GetAllClients());
        }

        /// <summary>
        /// Obtém um cliente específica pelo ID
        /// </summary>
        /// <param name="id">Identificador único do cliente</param>
        /// <returns>Detalhes</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _clientService.GetClientsById(id));
        }
        /// <summary>
        /// Cria um novo cliente
        /// </summary>
        /// <param name="value">Dados do cliente a ser criado</param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClientDto value)
        {
            return Ok(await _clientService.CreateClients(value));
        }
        /// <summary>
        /// Salva um token do app para o cliente
        /// </summary>
        /// <param name="value">Dados do token e idClient</param>
        [HttpPost("TokenApp")]
        public async Task<ActionResult> PostTokenApp([FromBody] TokenAppRequest value)
        {
            return Ok(await _clientService.PostTokenApp(value));
        }
        /// <summary>
        /// Atualiza um cliente existente
        /// </summary>
        /// <param name="value">Novos dados</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ClientDto value)
        {
            await _clientService.UpdateClients(value);
            return Ok(true);
        }
        /// <summary>
        /// Excluir um cliente existente
        /// </summary>
        /// <param name="id">Identificador único do cliente</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult>  Delete(int id)
        {
            await _clientService.DeleteClients(id);
            return Ok(true);
        }
    }
}
