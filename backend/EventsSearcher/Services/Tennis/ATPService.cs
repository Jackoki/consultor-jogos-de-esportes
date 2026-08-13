using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Models.Tennis;
using consultor_jogos_de_esportes.Utils;
using consultor_jogos_de_esportes.Utils.LogoHelper;

namespace consultor_jogos_de_esportes.Services.Tennis
{
    public class ATPService : ApiSportService
    {
        public override string SportName => "atp"; 
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ATPService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var dates = GetDatesFilter(filter);
            var urls = BuildUrls(dates, filter.DateFilterType);
            var tasks = urls.Select(url => GetAsync<ATPResponse>(url));
            var responses = await Task.WhenAll(tasks);

            var competitions = responses
                .Where(r => r != null)
                .SelectMany(r => r!.Events)
                .ToList();

            return MapToSportEvents(competitions, dates);
        }

        private List<string> BuildUrls(DateRange dates, DateFilterType filterType)
        {
            if (filterType == DateFilterType.Today || filterType == DateFilterType.SpecificDate)
            {
                return new() {
                    $"https://site.api.espn.com/apis/site/v2/sports/tennis/atp/scoreboard?dates={dates.StartDate:yyyyMMdd}"
                };
            }

            var urls = new List<string>();
            for (var data = dates.StartDate.Date; data <= dates.EndDate.Date; data = data.AddDays(1))
            {
                urls.Add($"https://site.api.espn.com/apis/site/v2/sports/tennis/atp/scoreboard?dates={data:yyyyMMdd}");
            }

            return urls;
        }

        private List<SportEventModel> MapToSportEvents(List<ATPModel> events, DateRange dates)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var eventsSport = new List<SportEventModel>();

            foreach (var atpEvent in events)
            {
                foreach (var competition in atpEvent.Groupings)
                {
                    foreach(var match in competition.Competitions)
                    {
                        var beginDate = DateTimeUtils.ToBrazilTime(match.Date);

                        if (beginDate.Date < dates.StartDate.Date || beginDate.Date > dates.EndDate.Date.AddDays(-1))
                        {
                            continue;
                        }

                        var player1 = match.Competitors.ElementAtOrDefault(0);
                        var player2 = match.Competitors.ElementAtOrDefault(1);

                        if (player1 == null || player2 == null)
                            continue;

                        var leftCode = CountryHelper.GetAlpha2FromCountryName(player1.Athlete.Flag.FlagName);
                        var rightCode = CountryHelper.GetAlpha2FromCountryName(player2.Athlete.Flag.FlagName);

                        var leftImage = !string.IsNullOrWhiteSpace(leftCode) ? $"{baseUrl}{CountryFlagHelper.GetCountryFlag(leftCode)}" : null;
                        var rightImage = !string.IsNullOrWhiteSpace(rightCode) ? $"{baseUrl}{CountryFlagHelper.GetCountryFlag(rightCode)}" : null;

                        string player1Name = string.IsNullOrEmpty(player1.Athlete.DisplayName) ? "TBD" : player1.Athlete.DisplayName;
                        string player2Name = string.IsNullOrEmpty(player2.Athlete.DisplayName) ? "TBD" : player2.Athlete.DisplayName;

                        eventsSport.Add(new SportEventModel
                        {
                            SportName = "Tênis ATP: " + atpEvent.EventName,
                            EventName = competition.Grouping.DisplayName + " - " + match.Round.DisplayName + ":\n " + $"{player1Name} vs {player2Name}",
                            BeginDate = beginDate,
                            EndDate = beginDate.AddHours(3),
                            Location = match.Venue.Name,
                            HasTime = true,
                            LeftImage = leftImage,
                            RightImage = rightImage
                        });
                    }
                }
            }
            return eventsSport;
        }
    }
}