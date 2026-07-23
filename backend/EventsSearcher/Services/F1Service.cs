using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.Services
{
    public class F1Service : ApiSportService
    {
        public F1Service(HttpClient httpClient) : base(httpClient)
        {

        }

        public override async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var dates = GetDatesFilter(filter);
            var url = BuildUrl(dates, filter.DateFilterType);
            var f1Events = await GetAsync<List<F1Model>>(url) ?? new List<F1Model>();
            f1Events = FilterEventsByDate(f1Events, filter);
            return MapToSportEvents(f1Events);
        }

        private List<F1Model> FilterEventsByDate(List<F1Model> events, DTOFilterDates filter)
        {
            switch (filter.DateFilterType)
            {
                case DateFilterType.Today:
                {
                    var today = DateTime.UtcNow.Date;
                    return events.Where(e => e.DateTimeStart.Date <= today && e.DateTimeEnd.Date >= today).ToList();
                }

                case DateFilterType.SpecificDate:
                {
                    var date = filter.Date!.Value.Date;
                    return events.Where(e => e.DateTimeStart.Date <= date && e.DateTimeEnd.Date >= date).ToList();
                }

                case DateFilterType.Week:
                {
                    var weekRange = GetDatesFilter(filter);
                    return events.Where(e => e.DateTimeStart.Date <= weekRange.EndDate && e.DateTimeEnd.Date >= weekRange.StartDate).ToList();
                }

                default:
                    return events;
            }
        }

        private string BuildUrl(DateRange dates, DateFilterType filterType)
        {
            if (filterType == DateFilterType.Today || filterType == DateFilterType.SpecificDate)
            {
                return $"https://api.openf1.org/v1/meetings" + $"?date_start<={dates.StartDate:yyyy-MM-dd}" + $"&date_end>={dates.StartDate:yyyy-MM-dd}";
            }

            return $"https://api.openf1.org/v1/meetings" + $"?date_start<={dates.EndDate:yyyy-MM-dd}" + $"&date_end>={dates.StartDate:yyyy-MM-dd}";
        }

        private List<SportEventModel> MapToSportEvents(List<F1Model> f1Events)
        {
            return f1Events.Select(m => new SportEventModel
            {
                SportName = "F1",
                EventName = m.NameEvent,
                BeginDate = DateTimeUtils.ToBrazilTime(m.DateTimeStart),
                EndDate = DateTimeUtils.ToBrazilTime(m.DateTimeEnd),
                Location = CountryHelper.GetCountryName(m.CountryName),
                CentralImage = m.CountryFlag,
                HasTime = true
            }).ToList();
        }
    }
}