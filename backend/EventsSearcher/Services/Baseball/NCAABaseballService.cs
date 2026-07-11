using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.DTOs.NCAA;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.Services.Baseball
{
    public class NCAAService : ApiSportService
    {
        public NCAAService(HttpClient httpClient) : base(httpClient)
        {

        }
        public override async Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter)
        {
            var dates = GetDatesFilter(filter);

            var games = new List<NCAABaseballModel>();

            for (var date = dates.StartDate.Date; date <= dates.EndDate.Date; date = date.AddDays(1))
            {
                var url = BuildUrl(date);

                var response = await GetAsync<NCAAScoreboardResponse>(url);

                if (response?.Games != null)
                {
                    games.AddRange(response.Games.Select(x => x.Game));
                }
            }

            return MapToSportEvents(games);
        }

        private string BuildUrl(DateTime date)
        {
            return $"https://ncaa-api.henrygd.me/scoreboard/baseball/d1/{date:yyyy/MM/dd}/all-conf";
        }

        private List<SportEventModel> MapToSportEvents(List<NCAABaseballModel> games)
        {
            return games.Select(g => new SportEventModel
            {
                SportName = "Baseball NCAA",
                EventName = $"{g.Away.Names.Short} x {g.Home.Names.Short}",
                BeginDate = DateTimeUtils.ToBrazilTime(DateTimeOffset.FromUnixTimeSeconds(g.StartTimeEpoch).UtcDateTime),
                EndDate = DateTimeUtils.ToBrazilTime(DateTimeOffset.FromUnixTimeSeconds(g.StartTimeEpoch).UtcDateTime.AddHours(3)),
                Location = "Estados Unidos",
                HasTime = true
            }).ToList();
        }
    }
}