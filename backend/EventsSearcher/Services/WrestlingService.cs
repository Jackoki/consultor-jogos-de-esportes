using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;

namespace consultor_jogos_de_esportes.Services
{
    public class WrestlingService : ISportService
    {
        public string SportName => "wrestling";
        private readonly WWEService _wwe;

        public WrestlingService(WWEService wwe)
        {
            _wwe = wwe;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {

            if (!string.IsNullOrEmpty(filter.League))
            {
                switch (filter.League.ToLower())
                {
                    case "wwe":
                        return await _wwe.GetEventsAsync(filter);

                    default:
                        throw new ArgumentException("Liga de baseball inválida");
                }
            }

            var wwe = await _wwe.GetEventsAsync(filter);
            return wwe.OrderBy(x => x.BeginDate).ToList();
        }
    }
}