using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace categoria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsCategoryController : ControllerBase
    {
        private readonly ISportsCategoryService _sportsCategoryService;
        public SportsCategoryController(ISportsCategoryService sportsCategoryService)
        {
            _sportsCategoryService = sportsCategoryService;
        }
        /// <summary>
        /// Obtém todas as categorias esportivas disponíveis 
        /// </summary>
        /// <returns>Lista de categorias esportivas</returns>

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllSportsCategoryByIdCourt()
        {
            return Ok(await _sportsCategoryService.GetAllSportsCategory());
        }

        /// <summary>
        /// Obtém uma categoria esportiva específica pelo ID
        /// </summary>
        /// <param name="id">Identificador único da categoria</param>
        /// <returns>Detalhes da categoria esportiva</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _sportsCategoryService.GetSportsCategoryById(id));
        }
        /// <summary>
        /// Cria uma nova categoria esportiva
        /// </summary>
        /// <param name="value">Dados da categoria a ser criada</param>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SportsCategoryDto value)
        {
            await _sportsCategoryService.CreateSportsCategory(value);
            return Ok(true);
        }

        /// <summary>
        /// Atualiza uma categoria esportiva existente
        /// </summary>
        /// <param name="id">Identificador único da categoria</param>
        /// <param name="value">Novos dados da categoria</param>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] SportsCategoryDto value)
        {
            await _sportsCategoryService.UpdateSportsCategory(value);
            return Ok(true);
        }
    }
}
