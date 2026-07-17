using consultor_jogos_de_esportes.HealthChecks;
using Microsoft.AspNetCore.Mvc;

namespace consultor_jogos_de_esportes.Controllers
{
    [ApiController]
    [Route("api/health-check")]
    public class HealthController : ControllerBase
    {
        private readonly ApiHealthManager apiHealthManager;

        public HealthController(ApiHealthManager apiHealthManager)
        {
            this.apiHealthManager = apiHealthManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await apiHealthManager.ValidateAllAsync();

            return Ok(result);
        }
    }
}
