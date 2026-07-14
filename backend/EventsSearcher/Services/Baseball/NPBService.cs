using System.Text;
using System.Text.Json;
using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Models.Baseball.NPB;
using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.Services.Baseball
{
    public class NPBService : ISportService
    {
        private readonly HttpClient _httpClient;

        public NPBService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            DateRange dates = GetDatesFilter(filter);

            var chessEvents = await FetchChessEvents(dates);

            return MapToSportEvents(chessEvents);
        }

        private async Task<List<NPBModel>> FetchChessEvents(DateRange dates)
        {
            var body = new
            {
                date_start = dates.StartDate,
                date_end = dates.EndDate
            };

            var json = JsonSerializer.Serialize(body);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:3000/npb/events", content);

            if (!response.IsSuccessStatusCode)
                return new List<NPBModel>();

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<NPBModel>>(responseJson) ?? new List<NPBModel>();
        }

        private List<SportEventModel> MapToSportEvents(List<NPBModel> events)
        {
            return events.Select(e => new SportEventModel
            {
                SportName = "Baseball NPB",
                EventName = e.Name,
                BeginDate = DateTimeUtils.ToBrazilTime(e.Date),
                EndDate = DateTimeUtils.ToBrazilTime(e.Date),
                Location = "Japão",
                HasTime = false
            }).ToList();
        }

        private DateRange GetDatesFilter(DTOFilterDates filter)
        {
            DateTime startDate;
            DateTime endDate;

            switch (filter.DateFilterType)
            {
                case DateFilterType.Today:
                    startDate = DateTime.Today;
                    endDate = startDate.AddDays(1);
                    break;

                case DateFilterType.SpecificDate:
                    if (!filter.Date.HasValue)
                        throw new ArgumentException("Data não informada.");

                    startDate = filter.Date.Value.Date;
                    endDate = startDate.AddDays(1);
                    break;

                case DateFilterType.Week:
                    if (!filter.Date.HasValue)
                        throw new ArgumentException("Data não informada.");

                    var date = filter.Date.Value.Date;

                    startDate = date.AddDays(-(int)date.DayOfWeek);
                    endDate = startDate.AddDays(7);

                    break;

                default:
                    throw new ArgumentException("Filtro inválido.");
            }

            return new DateRange
            {
                StartDate = startDate,
                EndDate = endDate
            };
        }
    }
}