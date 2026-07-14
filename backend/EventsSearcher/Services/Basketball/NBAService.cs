using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Models.Baseball;
using consultor_jogos_de_esportes.Models.Basketball;
using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.Services.Basketball
{
    public class NBAService : ApiSportService
    {
        public NBAService(HttpClient httpClient) : base(httpClient)
        {
        }

        public override async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var dates = GetDatesFilter(filter);
            var url = BuildUrl(dates);
            var response = await GetAsync<NBAResponse>(url);

            if (response == null)
                return new List<SportEventModel>();

            var competitions = response.Events.SelectMany(e => e.Competitions).ToList();

            competitions = FilterEventsByDate(competitions, filter);

            return MapToSportEvents(competitions);
        }

        private List<NBACompetition> FilterEventsByDate(List<NBACompetition> events, DTOFilterDates filter)
        {
            switch (filter.DateFilterType)
            {
                case DateFilterType.Today:
                    {
                        var today = DateTime.UtcNow.Date;

                        return events.Where(e => e.Date.Date == today).ToList();
                    }

                case DateFilterType.SpecificDate:
                    {
                        var date = filter.Date!.Value.Date;

                        return events.Where(e => e.Date.Date == date).ToList();
                    }

                case DateFilterType.Week:
                    {
                        var weekRange = GetDatesFilter(filter);

                        return events.Where(e => e.Date.Date >= weekRange.StartDate && e.Date.Date <= weekRange.EndDate).ToList();
                    }

                default:
                    return events;
            }
        }

        private string BuildUrl(DateRange dates)
        {
            return $"https://site.api.espn.com/apis/site/v2/sports/basketball/nba/scoreboard?dates={dates.StartDate:yyyyMMdd}";
        }

        private List<SportEventModel> MapToSportEvents(List<NBACompetition> competitions)
        {
            var events = new List<SportEventModel>();

            foreach (var competition in competitions)
            {
                var home = competition.Competitors.FirstOrDefault(c => c.HomeAway == "home");

                var away = competition.Competitors.FirstOrDefault(c => c.HomeAway == "away");

                if (home == null || away == null)
                    continue;

                events.Add(new SportEventModel
                {
                    SportName = "Basquete",
                    EventName = $"{away.Team.DisplayName} vs {home.Team.DisplayName}",
                    BeginDate = DateTimeUtils.ToBrazilTime(competition.Date),
                    EndDate = DateTimeUtils.ToBrazilTime(competition.Date),
                    Location = competition.Venue.Address.City,
                    HasTime = true
                });
            }

            return events;
        }
    }
}