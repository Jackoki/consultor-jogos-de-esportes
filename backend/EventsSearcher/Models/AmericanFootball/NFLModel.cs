using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.AmericanFootball
{
    public class NFLModel
    {
        [JsonPropertyName("competitions")]
        public List<NFLCompetition> Competitions { get; set; } = new();
    }

    public class NFLCompetition
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("venue")]
        public NFLVenue Venue { get; set; } = new();

        [JsonPropertyName("competitors")]
        public List<NFLCompetitor> Competitors { get; set; } = new();
    }

    public class NFLVenue
    {
        [JsonPropertyName("fullName")]
        public string Stadium { get; set; }

        [JsonPropertyName("address")]
        public NFLAddress Address { get; set; } = new();
    }

    public class NFLAddress
    {
        [JsonPropertyName("city")]
        public string City { get; set; }
    }

    public class NFLCompetitor
    {
        [JsonPropertyName("homeAway")]
        public string HomeAway { get; set; } = "";

        [JsonPropertyName("team")]
        public NFLTeam Team { get; set; } = new();
    }

    public class NFLTeam
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = "";

        [JsonPropertyName("logo")]
        public string Logo { get; set; } = "";
    }
}