

using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Services;
using Microsoft.AspNetCore.Mvc;

namespace consultor_jogos_de_esportes.Controllers
{
    [ApiController]
    [Route("api/basketball")]
    public class BasketballController : ControllerBase
    {
        private readonly BaseballService _basketballService;

        public BasketballController(BaseballService basketballService)
        {
            this._basketballService = basketballService;
        }

        [HttpPost("events")]
        public async Task<IActionResult> GetEvents([FromBody] DTOFilterDates filter)
        {
            try
            {
                var result = await _basketballService.GetEventsAsync(filter);
                return Ok(result);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

    }
}
