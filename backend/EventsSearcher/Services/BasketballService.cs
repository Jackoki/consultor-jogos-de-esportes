using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Services.Basketball;

namespace consultor_jogos_de_esportes.Services
{
    public class BasketballService : ISportService
    {
        public string SportName => "basketball";
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