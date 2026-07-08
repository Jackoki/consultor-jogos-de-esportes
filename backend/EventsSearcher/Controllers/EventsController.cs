using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Services;
using Microsoft.AspNetCore.Mvc;

namespace consultor_jogos_de_esportes.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEnumerable<ISportService> _services;

        public EventsController(IEnumerable<ISportService> services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> GetEvents([FromBody] DTOFilterDates filter)
        {
            var tasks = _services.Select(s => s.GetEventsAsync(filter));

            var results = await Task.WhenAll(tasks);

            var events = results
                .SelectMany(x => x)
                .OrderBy(e => e.BeginDate)
                .ToList();

            return Ok(events);
        }
    }
}