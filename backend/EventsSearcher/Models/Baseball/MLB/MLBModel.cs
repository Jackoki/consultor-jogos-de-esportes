using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.Baseball
{
    public class MLBModel
    {
        [JsonPropertyName("gamePk")]
        public int GamePk { get; set; }

        [JsonPropertyName("gameDate")]
        public DateTime GameDate { get; set; }

        [JsonPropertyName("teams")]
        public MLBTeams Teams { get; set; } = new();

        [JsonPropertyName("venue")]
        public MLBVenue Venue { get; set; } = new();

        public DateTime DateTimeStart => GameDate;

        public DateTime DateTimeEnd => GameDate.AddHours(4);

        public string NameEvent =>$"{Teams.Away.Team.Name} x {Teams.Home.Team.Name}";

        public string CountryName => "USA";
    }

    public class MLBTeams
    {
        [JsonPropertyName("away")]
        public MLBTeamSide Away { get; set; } = new();

        [JsonPropertyName("home")]
        public MLBTeamSide Home { get; set; } = new();
    }

    public class MLBTeamSide
    {
        [JsonPropertyName("team")]
        public MLBTeam Team { get; set; } = new();
    }

    public class MLBTeam
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class MLBVenue
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}