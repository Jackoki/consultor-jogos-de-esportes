using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Services.Motorsport;

namespace consultor_jogos_de_esportes.Services
{
    public class MotorsportService : ISportService
    {
        public string SportName => "motorsport";
        private readonly F1Service _f1;

        public MotorsportService(F1Service f1)
        {
            _f1 = f1;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var f1 = await _f1.GetEventsAsync(filter);

            return f1.OrderBy(x => x.BeginDate).ToList();
        }
    }
}