using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Services.Baseball;
using consultor_jogos_de_esportes.Services.Basketball;
using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.Services
{
    public class AmericanFootballService : ISportService
    {
        public string SportName => "american-football";
        private readonly NFLService _nfl;

        public AmericanFootballService(NFLService nfl)
        {
            _nfl = nfl;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var nfl = await _nfl.GetEventsAsync(filter);

            return nfl.OrderBy(x => x.BeginDate).ToList();
        }
    }
}