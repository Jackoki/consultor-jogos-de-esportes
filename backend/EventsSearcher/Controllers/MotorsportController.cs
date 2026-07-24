
using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Services;
using consultor_jogos_de_esportes.Services.Motorsport;
using Microsoft.AspNetCore.Mvc;

namespace consultor_jogos_de_esportes.Controllers
{
    [ApiController]
    [Route("api/motorsport")]
    public class MotorsportController : ControllerBase
    {
        private readonly MotorsportService _motorsportService;
        public MotorsportController(MotorsportService motorsportService) { 
            this._motorsportService = motorsportService;
        }

        [HttpPost("events")]
        public async Task<IActionResult> GetEvents([FromBody] DTOFilterDates filter)
        {
            try
            {
                var result = await _motorsportService.GetEventsAsync(filter);
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
