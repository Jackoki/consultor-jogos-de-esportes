using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Services.Hockey;

namespace consultor_jogos_de_esportes.Services
{
    public class HockeyService : ISportService
    {
        public string SportName => "hockey";
        private readonly NHLService _nhl;

        public HockeyService(NHLService nfl)
        {
            _nhl = nfl;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var nfl = await _nhl.GetEventsAsync(filter);

            return nfl.OrderBy(x => x.BeginDate).ToList();
        }
    }
}