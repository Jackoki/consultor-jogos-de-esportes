

using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Services;
using Microsoft.AspNetCore.Mvc;

namespace consultor_jogos_de_esportes.Controllers
{
    [ApiController]
    [Route("api/hockey")]
    public class HockeyController : ControllerBase
    {
        private readonly HockeyService _hockeyService;

        public HockeyController(HockeyService hockeyService)
        {
            this._hockeyService = hockeyService;
        }

        [HttpPost("events")]
        public async Task<IActionResult> GetEvents([FromBody] DTOFilterDates filter)
        {
            try
            {
                var result = await _hockeyService.GetEventsAsync(filter);
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
