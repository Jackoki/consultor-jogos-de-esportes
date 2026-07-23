using System;
using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Models.Baseball;
using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.Services.Baseball
{
    public class MLBService : ApiSportService
    {
        public MLBService(HttpClient httpClient) : base(httpClient)
        {

        }

        public override async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var dates = GetDatesFilter(filter);
            var url = BuildUrl(dates, filter.DateFilterType); 
            var response = await GetAsync<MLBResponse>(url);
            var baseballEvents = response?.Dates.SelectMany(x => x.Games).GroupBy(x => x.GamePk).Select(x => x.First()).ToList() ?? new List<MLBModel>();
            baseballEvents = FilterEventsByDate(baseballEvents, filter);
            return MapToSportEvents(baseballEvents);
        }

        private List<MLBModel> FilterEventsByDate(List<MLBModel> events, DTOFilterDates filter)
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
                    return events.Where(e => e.DateTimeStart.Date == date || e.DateTimeEnd.Date == date).ToList();
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
            return $"https://statsapi.mlb.com/api/v1/schedule?sportId=1" + $"&startDate={dates.StartDate:yyyy-MM-dd}" + $"&endDate={dates.EndDate:yyyy-MM-dd}";
        }

        private List<SportEventModel> MapToSportEvents(List<MLBModel> baseballEvents)
        {
            return baseballEvents.Select(m => new SportEventModel
            {
                SportName = "Beisebol MLB",
                EventName = m.NameEvent,
                BeginDate = DateTimeUtils.ToBrazilTime(m.DateTimeStart),
                EndDate = DateTimeUtils.ToBrazilTime(m.DateTimeEnd),
                Location = CountryHelper.GetCountryName(m.Venue.Name),
                LeftImage = $"https://www.mlbstatic.com/team-logos/{m.Teams.Away.Team.Id}.svg",
                RightImage = $"https://www.mlbstatic.com/team-logos/{m.Teams.Home.Team.Id}.svg",
                HasTime = true
            }).ToList();
        }
    }
}