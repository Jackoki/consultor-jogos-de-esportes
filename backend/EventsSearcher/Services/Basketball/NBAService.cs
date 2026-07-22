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
            var urls = BuildUrls(dates, filter.DateFilterType);
            var tasks = urls.Select(url => GetAsync<NBAResponse>(url));
            var responses = await Task.WhenAll(tasks);

            var competitions = responses
                .Where(r => r != null)
                .SelectMany(r => r!.Events)
                .SelectMany(e => e.Competitions)
                .ToList();

            return MapToSportEvents(competitions);
        }

        private List<string> BuildUrls(DateRange dates, DateFilterType filterType)
        {
            if (filterType == DateFilterType.Today || filterType == DateFilterType.SpecificDate)
            {
                return new() {
                    $"https://site.api.espn.com/apis/site/v2/sports/basketball/nba/scoreboard?dates={dates.StartDate:yyyyMMdd}"
                };
            }

            var urls = new List<string>();

            for (var data = dates.StartDate.Date; data <= dates.EndDate.Date; data = data.AddDays(1))
            {
                urls.Add($"https://site.api.espn.com/apis/site/v2/sports/basketball/nba/scoreboard?dates={data:yyyyMMdd}");
            }

            return urls;
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
                    SportName = "Basquete NBA",
                    EventName = $"{home.Team.DisplayName} vs {away.Team.DisplayName}",
                    BeginDate = DateTimeUtils.ToBrazilTime(competition.Date),
                    EndDate = DateTimeUtils.ToBrazilTime(competition.Date),
                    Location = competition.Venue.Address.City,
                    HasTime = true,
                    LeftImage = home.Team.Logo,
                    RightImage = away.Team.Logo
                });
            }

            return events;
        }
    }
}