using System.Text;
using System.Text.Json;
using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Utils;
using consultor_jogos_de_esportes.Utils.LogoHelper;

namespace consultor_jogos_de_esportes.Services
{
    public class ChessService : ISportService
    {
        public string SportName => "chess";
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChessService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            DateRange dates = GetDatesFilter(filter);

            var chessEvents = await FetchChessEvents(dates);

            return MapToSportEvents(chessEvents);
        }

        private async Task<List<ChessModel>> FetchChessEvents(DateRange dates)
        {
            var body = new
            {
                date_start = dates.StartDate,
                date_end = dates.EndDate
            };

            var json = JsonSerializer.Serialize(body);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:3000/chess/events", content);

            if (!response.IsSuccessStatusCode)
                return new List<ChessModel>();

            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ChessModel>>(responseJson) ?? new List<ChessModel>();
        }

        private List<SportEventModel> MapToSportEvents(List<ChessModel> events)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            return events.Select(e => new SportEventModel
            {
                SportName = "Xadrez",
                EventName = e.NameEvent,
                BeginDate = DateTimeUtils.ToBrazilTime(e.DateTimeStart),
                EndDate = DateTimeUtils.ToBrazilTime(e.DateTimeEnd),
                Location = e.CountryName,
                CentralImage = $"{baseUrl}{CountryFlagHelper.GetCountryFlag(e.CountryCodeAlpha2)}",
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