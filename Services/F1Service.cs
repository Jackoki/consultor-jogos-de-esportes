using System.Text.Json;
using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.Services
{
    public class F1Service : ISportService
    {
        private readonly HttpClient _httpClient;

        public F1Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            switch (filter.DateFilterType)
            {
                case DateFilterType.Today:
                    return new List<SportEventModel>();

                case DateFilterType.Week:
                    return new List<SportEventModel>();

                case DateFilterType.SpecificDate:
                    return new List<SportEventModel>();

                default:
                    throw new ArgumentException("Data não válida para filtragem");
            }
        }
    }
}
