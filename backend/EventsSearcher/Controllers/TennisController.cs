

using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Services;
using Microsoft.AspNetCore.Mvc;

namespace consultor_jogos_de_esportes.Controllers
{
    [ApiController]
    [Route("api/tennis")]
    public class TennisController : ControllerBase
    {
        private readonly TennisService _tennisService;

        public TennisController(TennisService tennisService)
        {
            this._tennisService = tennisService;
        }

        [HttpPost("events")]
        public async Task<IActionResult> GetEvents([FromBody] DTOFilterDates filter)
        {
            try
            {
                var result = await _tennisService.GetEventsAsync(filter);
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
