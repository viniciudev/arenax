using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Quadra.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtEvaluationsController : ControllerBase
    {
        private readonly ICourtEvaluationsService _courtEvaluationsService;
        public CourtEvaluationsController(ICourtEvaluationsService courtEvaluationsService)
        {
            _courtEvaluationsService = courtEvaluationsService;
        }
        /// <summary>
        /// Obtém todas as avaliações disponíveis ligadas a quadra
        /// </summary>
        /// <returns>Lista de avaliações</returns>
        /// <param name="id">Identificador único da quadra</param>
        [HttpGet("GetAll/{id}")]
        public async Task<ActionResult> GetAllCourtEvaluationsByIdCourt(int id)
        {
            return Ok(await _courtEvaluationsService.GetAllCourtEvaluationsByIdCourt(id));
        }

        /// <summary>
        /// Obtém uma avaliação específica pelo ID
        /// </summary>
        /// <param name="id">Identificador único da avaliação</param>
        /// <returns>Detalhes da avaliação</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _courtEvaluationsService.GetCourtEvaluationsById(id));
        }
        /// <summary>
        /// Cria uma nova avaliação da quadra esportiva
        /// </summary>
        /// <param name="value">Dados da avaliação a ser criada</param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CourtEvaluationsDto value)
        {
            await _courtEvaluationsService.CreateCourtEvaluations(value);
            return Ok(true);
        }

        /// <summary>
        /// Atualiza uma avaliação da quadra esportiva existente
        /// </summary>
        /// <param name="id">Identificador único da avaliação</param>
        /// <param name="value">Novos dados da avaliação</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CourtEvaluationsDto value)
        {
            await _courtEvaluationsService.UpdateCourtEvaluations(value);
            return Ok(true);
        }
    }
}
