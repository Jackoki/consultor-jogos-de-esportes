using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Models.Fighting;
using consultor_jogos_de_esportes.Utils;
using consultor_jogos_de_esportes.Utils.LogoHelper;

namespace consultor_jogos_de_esportes.Services.Fighting
{
    public class UFCService : ApiSportService
    {
        public override string SportName => "ufc";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UFCService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var dates = GetDatesFilter(filter);
            var urls = BuildUrls(dates, filter.DateFilterType);
            var tasks = urls.Select(url => GetAsync<UFCResponse>(url));
            var responses = await Task.WhenAll(tasks);

            var events = responses
                .Where(r => r != null)
                .SelectMany(r => r!.Events)
                .ToList();

            return MapToSportEvents(events);
        }

        private List<string> BuildUrls(DateRange dates, DateFilterType filterType)
        {
            if (filterType == DateFilterType.Today || filterType == DateFilterType.SpecificDate)
            {
                return new() {
                    $"https://site.api.espn.com/apis/site/v2/sports/mma/ufc/scoreboard?dates={dates.StartDate:yyyyMMdd}"
                };
            }

            var urls = new List<string>();

            for (var data = dates.StartDate.Date; data <= dates.EndDate.Date; data = data.AddDays(1))
            {
                urls.Add($"https://site.api.espn.com/apis/site/v2/sports/mma/ufc/scoreboard?dates={data:yyyyMMdd}");
            }

            return urls;
        }

        private List<SportEventModel> MapToSportEvents(List<UFCModel> events)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var eventsSport = new List<SportEventModel>();

            foreach (var ufcEvent in events)
            {
                foreach (var competition in ufcEvent.Competitions)
                {
                    var athletes = competition.Competitors.Where(c => c.Athlete != null).ToList();

                    if (athletes.Count < 2)
                        continue;

                    var fighter1 = athletes[0].Athlete;
                    var fighter2 = athletes[1].Athlete;

                    var leftCode = CountryHelper.GetAlpha2FromCountryName(fighter1.Flag.CountryName);
                    var rightCode = CountryHelper.GetAlpha2FromCountryName(fighter2.Flag.CountryName);

                    var leftImage = !string.IsNullOrWhiteSpace(leftCode) ? $"{baseUrl}{CountryFlagHelper.GetCountryFlag(leftCode)}" : null;
                    var rightImage = !string.IsNullOrWhiteSpace(rightCode) ? $"{baseUrl}{CountryFlagHelper.GetCountryFlag(rightCode)}": null;

                    eventsSport.Add(new SportEventModel
                    {
                        SportName = "UFC",
                        EventName = ufcEvent.ShortName + ": " + $"{fighter1.DisplayName} vs {fighter2.DisplayName}",
                        BeginDate = DateTimeUtils.ToBrazilTime(competition.Date),
                        EndDate = DateTimeUtils.ToBrazilTime(competition.Date).AddHours(3),
                        Location = competition.Venue.Address.City,
                        HasTime = true,
                        LeftImage = leftImage,
                        RightImage = rightImage
                    });
                }
            }

            return eventsSport;
        }
    }
}