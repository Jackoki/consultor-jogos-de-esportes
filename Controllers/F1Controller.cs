

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
        public async Task<IActionResult> GetEvents(int year)
        {
            var result = await _f1Service.GetMeetingsAsync(year);

            if(result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
