

using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Services;
using Microsoft.AspNetCore.Mvc;

namespace consultor_jogos_de_esportes.Controllers
{
    [ApiController]
    [Route("api/american-football")]
    public class AmericanFootballController : ControllerBase
    {
        private readonly AmericanFootballService _americanFootballService;

        public AmericanFootballController(AmericanFootballService americanFootballService)
        {
            this._americanFootballService = americanFootballService;
        }

        [HttpPost("events")]
        public async Task<IActionResult> GetEvents([FromBody] DTOFilterDates filter)
        {
            try
            {
                var result = await _americanFootballService.GetEventsAsync(filter);
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
