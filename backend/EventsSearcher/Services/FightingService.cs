using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Services.Fighting;

namespace consultor_jogos_de_esportes.Services
{
    public class FightingService : ISportService
    {
        public string SportName => "ufc";
        private readonly UFCService _ufc;

        public FightingService(UFCService ufc)
        {
            _ufc = ufc;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var nfl = await _ufc.GetEventsAsync(filter);

            return nfl.OrderBy(x => x.BeginDate).ToList();
        }
    }
}