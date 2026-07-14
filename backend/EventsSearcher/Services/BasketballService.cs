using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Services.Baseball;
using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.Services
{
    public class BasketballService : ISportService
    {
        private readonly NBAService _nba;

        public BasketballService(NBAService nba)
        {
            _nba = nba;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var nba = await _nba.GetEventsAsync(filter);

            return nba.OrderBy(x => x.BeginDate).ToList();
        }
    }
}