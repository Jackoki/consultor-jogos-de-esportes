using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Models.Tennis;
using consultor_jogos_de_esportes.Utils;
using consultor_jogos_de_esportes.Utils.LogoHelper;
using Microsoft.AspNetCore.Http;

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
                .SelectMany(e => e.Groupings)
                .SelectMany(g => g.Competitions)
                .ToList();

            return MapToSportEvents(competitions);
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

        private List<SportEventModel> MapToSportEvents(List<ATPCompetition> competitions)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var events = new List<SportEventModel>();

            foreach (var competition in competitions)
            {
                var player1 = competition.Competitors.ElementAtOrDefault(0);
                var player2 = competition.Competitors.ElementAtOrDefault(1);

                if (player1 == null || player2 == null)
                    continue;

                var beginDate = DateTimeUtils.ToBrazilTime(competition.Date);

                var leftCode = CountryHelper.GetAlpha2FromCountryName(player1.Athlete.Flag.FlagName);
                var rightCode = CountryHelper.GetAlpha2FromCountryName(player2.Athlete.Flag.FlagName);

                var leftImage = !string.IsNullOrWhiteSpace(leftCode) ? $"{baseUrl}{CountryFlagHelper.GetCountryFlag(leftCode)}" : null;
                var rightImage = !string.IsNullOrWhiteSpace(rightCode) ? $"{baseUrl}{CountryFlagHelper.GetCountryFlag(rightCode)}" : null;

                events.Add(new SportEventModel
                {
                    SportName = "Tênis ATP",
                    EventName = $"{player1.Athlete.DisplayName} vs {player2.Athlete.DisplayName}",
                    BeginDate = beginDate,
                    EndDate = beginDate.AddHours(3),
                    Location = competition.Venue.Name,
                    HasTime = true,
                    LeftImage = leftImage,
                    RightImage = rightImage
                });
            }

            return events;
        }
    }
}