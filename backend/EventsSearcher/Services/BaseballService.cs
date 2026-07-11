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

        public BaseballService(MLBService mlb)
        {
            _mlb = mlb;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var mlb = await _mlb.GetEventsAsync(filter);

            return mlb.OrderBy(x => x.BeginDate).ToList();
        }
    }
}