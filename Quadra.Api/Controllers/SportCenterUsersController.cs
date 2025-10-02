using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Quadra.Api.Controllers
{
    public class SportCenterUsersController : Controller
    {
        private readonly ISportsCenterUsersService _sportsCenterUsersService;
        public SportCenterUsersController(ISportsCenterUsersService sportsCenterUsersService)
        {
            _sportsCenterUsersService = sportsCenterUsersService;
        }
        /// <summary>
        /// Obtém um centro esportivo específico pelo IdUser
        /// </summary>
        /// <param name="id">Identificador único do usuário</param>
        /// <returns>Detalhes do centro esportiva</returns>
        [HttpGet("GetByIdUser/{id}")]
        public async Task<ActionResult> GetSportsCenterUsersByIdUser(int id)
        {
            var resp = await _sportsCenterUsersService.GetSportsCenterUsersByIdUser(id);
            if (resp == null)
            {
                return BadRequest("Usuário não possui ligação com a empresa");
            }
            return Ok(resp);
        }

    }
}
