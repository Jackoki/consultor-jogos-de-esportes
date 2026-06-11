

using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Services;
using Microsoft.AspNetCore.Mvc;

namespace consultor_jogos_de_esportes.Controllers
{
    [ApiController]
    [Route("api/f1")]
    public class F1Controller : ControllerBase
    {
        private readonly F1Service _f1Service;
        public F1Controller(F1Service f1Service) { 
            this._f1Service = f1Service;
        }

        [HttpGet("meetings/{year}")]
        public async Task<IActionResult> GetEvents([FromBody] DTOFilterDates filter)
        {
            var result = await _f1Service.GetEventsAsync(filter);

            if(result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
