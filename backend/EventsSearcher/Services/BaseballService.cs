using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Services.Baseball;
using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.Services
{
    public class BaseballService : ISportService
    {
        private readonly MLBService _mlb;
        private readonly NCAAService _ncaa;

        public BaseballService(MLBService mlb, NCAAService ncaa)
        {
            _mlb = mlb;
            _ncaa = ncaa;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var mlb = await _mlb.GetEventsAsync(filter);
            var ncaa = await _ncaa.GetEventsAsync(filter);

            return mlb.Concat(ncaa).OrderBy(x => x.BeginDate).ToList();
        }
    }
}