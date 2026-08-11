using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Services.Tennis;

namespace consultor_jogos_de_esportes.Services
{
    public class TennisService : ISportService
    {
        public string SportName => "tennis";
        private readonly ATPService _atp;

        public TennisService(ATPService atp)
        {
            _atp = atp;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var nba = await _atp.GetEventsAsync(filter);

            return nba.OrderBy(x => x.BeginDate).ToList();
        }
    }
}