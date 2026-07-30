

using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Services;
using Microsoft.AspNetCore.Mvc;

namespace consultor_jogos_de_esportes.Controllers
{
    [ApiController]
    [Route("api/fighting")]
    public class FightingController : ControllerBase
    {
        private readonly FightingService _fightingService;

        public FightingController(FightingService fightingService)
        {
            this._fightingService = fightingService;
        }

        [HttpPost("events")]
        public async Task<IActionResult> GetEvents([FromBody] DTOFilterDates filter)
        {
            try
            {
                var result = await _fightingService.GetEventsAsync(filter);
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
