using System.Text;
using System.Text.Json;
using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Utils;
using consultor_jogos_de_esportes.Utils.LogoHelper;

namespace consultor_jogos_de_esportes.Services
{
    public class WWEService : ISportService
    {
        public string SportName => "wwe";
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WWEService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            DateRange dates = GetDatesFilter(filter);

            var wweEvents = await FetchWweEvents(dates);

            return MapToSportEvents(wweEvents);
        }

        private async Task<List<WrestlingModel>> FetchWweEvents(DateRange dates)
        {
            var body = new
            {
                date_start = dates.StartDate,
                date_end = dates.EndDate
            };

            var json = JsonSerializer.Serialize(body);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:3000/wwe/events", content);

            if (!response.IsSuccessStatusCode)
                return new List<WrestlingModel>();

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<WrestlingModel>>(responseJson) ?? new List<WrestlingModel>();
        }

        private List<SportEventModel> MapToSportEvents(List<WrestlingModel> events)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            return events.Select(e => new SportEventModel
            {
                SportName = "WWE",
                EventName = e.NameEvent,
                BeginDate = DateTimeUtils.ToBrazilTime(e.DateTimeStart),
                EndDate = DateTimeUtils.ToBrazilTime(e.DateTimeEnd),
                Location = string.Join(", ", new[]{ e.City, e.State, e.CountryName }.Where(x => !string.IsNullOrWhiteSpace(x))),
                CentralImage = e.CentralImage,
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