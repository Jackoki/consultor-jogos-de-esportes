using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Models.Hockey;
using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.Services.Hockey
{
    public class NHLService : ApiSportService
    {
        public override string SportName => "nHl";
        public NHLService(HttpClient httpClient) : base(httpClient)
        {
        }

        public override async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var dates = GetDatesFilter(filter);
            var urls = BuildUrls(dates, filter.DateFilterType);
            var tasks = urls.Select(url => GetAsync<NHLResponse>(url));
            var responses = await Task.WhenAll(tasks);

            var games = responses.Where(r => r != null).SelectMany(r => r!.GameWeek).SelectMany(r => r!.Games);

            if (filter.DateFilterType == DateFilterType.SpecificDate)
            {
                var selectedDate = dates.StartDate.Date;
                games = games.Where(g => DateTimeUtils.ToBrazilTime(g.StartTimeUTC).Date == selectedDate);
            }

            return MapToSportEvents(games.ToList());
        }

        private List<string> BuildUrls(DateRange dates, DateFilterType filterType)
        {
            switch (filterType)
            {
                case DateFilterType.Today:
                case DateFilterType.SpecificDate:
                    return new(){
                        $"https://api-web.nhle.com/v1/schedule/{dates.StartDate:yyyy-MM-dd}"
                    };

                case DateFilterType.Week:
                    var sunday = dates.StartDate.Date.AddDays(-(int)dates.StartDate.DayOfWeek);
                    return new()
                    {
                        $"https://api-web.nhle.com/v1/schedule/{sunday:yyyy-MM-dd}"
                    };

                default:
                    return [];
            }
        }

        private List<SportEventModel> MapToSportEvents(List<NHLGame> games)
        {
            var events = new List<SportEventModel>();

            foreach (var game in games)
            {
                events.Add(new SportEventModel
                {
                    SportName = "Hoquei NHL",
                    EventName = game.NameEvent,
                    BeginDate = DateTimeUtils.ToBrazilTime(game.StartTimeUTC),
                    EndDate = DateTimeUtils.ToBrazilTime(game.StartTimeUTC).AddHours(3),
                    Location = game.Venue.Name,
                    HasTime = true,
                    LeftImage = game.HomeTeam.Logo,
                    RightImage = game.AwayTeam.Logo
                });
            }

            return events;
        }
    }
}