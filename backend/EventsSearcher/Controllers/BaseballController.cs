

using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Services;
using consultor_jogos_de_esportes.Services.Baseball;
using Microsoft.AspNetCore.Mvc;

namespace consultor_jogos_de_esportes.Controllers
{
    [ApiController]
    [Route("api/baseball")]
    public class BaseballController : ControllerBase
    {
        private readonly BaseballService _baseballService;

        public BaseballController(BaseballService baseballService) { 
            this._baseballService = baseballService;
        }

        [HttpPost("meetings")]
        public async Task<IActionResult> GetEvents([FromBody] DTOFilterDates filter)
        {
            try
            {
                var result = await _baseballService.GetEventsAsync(filter);
                return Ok(result);
            }

            catch(ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }  
        }

    }
}
