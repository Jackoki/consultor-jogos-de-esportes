using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.Baseball
{
    public class NBAModel
    {
        [JsonPropertyName("competitions")]
        public List<NBACompetition> Competitions { get; set; } = new();
    }

    public class NBACompetition
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("venue")]
        public NBAVenue Venue { get; set; } = new();

        [JsonPropertyName("competitors")]
        public List<NBACompetitor> Competitors { get; set; } = new();
    }

    public class NBAVenue
    {
        [JsonPropertyName("fullName")]
        public string Stadium { get; set; }

        [JsonPropertyName("address")]
        public NBAAddress Address { get; set; } = new();
    }

    public class NBAAddress
    {
        [JsonPropertyName("city")]
        public string City { get; set; }
    }

    public class NBACompetitor
    {
        [JsonPropertyName("homeAway")]
        public string HomeAway { get; set; } = "";

        [JsonPropertyName("team")]
        public NBATeam Team { get; set; } = new();
    }

    public class NBATeam
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = "";
    }
}