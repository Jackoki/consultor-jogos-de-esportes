using System;
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
            DateRange dates = GetDatesFilterF1Event(filter);
            String urlApiF1 = BuildUrl(dates);
            var f1Events = await FetchF1Events(urlApiF1);

            return MapToSportEvents(f1Events);
        }

        public DateRange GetDatesFilterF1Event(DTOFilterDates filter)
        {
            DateTime startDate;
            DateTime endDate;

            switch (filter.DateFilterType)
            {
                case DateFilterType.Today:
                    startDate = DateTime.UtcNow.Date;
                    endDate = startDate.AddDays(1);
                    break;

                case DateFilterType.Week:
                    if (!filter.Date.HasValue)
                        throw new ArgumentException("Date é obrigatória para SpecificDate");

                    var date = filter.Date.Value.Date;
                    int differenceDaysToBeginOfTheWeek = (int)date.DayOfWeek;

                    startDate = date.AddDays(-differenceDaysToBeginOfTheWeek);
                    endDate = startDate.AddDays(7);
                    break;

                case DateFilterType.SpecificDate:
                    if (!filter.Date.HasValue)
                        throw new ArgumentException("Date é obrigatória para SpecificDate");

                    startDate = filter.Date.Value.Date;
                    endDate = startDate.AddDays(1);
                    break;

                default:
                    throw new ArgumentException("Data não válida para filtragem");
            }

            return new DateRange { StartDate = startDate, EndDate = endDate };
        }

        private string BuildUrl(DateRange dates)
        {
            return $"https://api.openf1.org/v1/meetings" + $"?date_start>={dates.StartDate:yyyy-MM-dd}" + $"&date_end<={dates.EndDate:yyyy-MM-dd}";
        }

        private async Task<List<F1Model>> FetchF1Events(string urlApiF1)
        {
            var responseApi = await _httpClient.GetAsync(urlApiF1);

            if (!responseApi.IsSuccessStatusCode)
                return new List<F1Model>();

            var jsonApi = await responseApi.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<F1Model>>(jsonApi) ?? new List<F1Model>();
        }

        private List<SportEventModel> MapToSportEvents(List<F1Model> f1Events)
        {
            return f1Events.Select(m => new SportEventModel
            {
                SportName = "F1",
                EventName = m.NameEvent,
                BeginDate = m.DateTimeStart,
                EndDate = m.DateTimeEnd,
                Location = m.CountryName
            }).ToList();
        }
    }
}
