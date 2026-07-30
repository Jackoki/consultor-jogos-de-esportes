using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.Fighting
{
    public class UFCModel
    {
        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("competitions")]
        public List<UFCCompetition> Competitions { get; set; } = new();
    }

    public class UFCCompetition
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("venue")]
        public UFCVenue Venue { get; set; } = new();

        [JsonPropertyName("competitors")]
        public List<UFCCompetitor> Competitors { get; set; } = new();
    }

    public class UFCVenue
    {
        [JsonPropertyName("fullName")]
        public string Stadium { get; set; }

        [JsonPropertyName("address")]
        public UFCAddress Address { get; set; } = new();
    }

    public class UFCAddress
    {
        [JsonPropertyName("city")]
        public string City { get; set; }
    }

    public class UFCCompetitor
    {
        [JsonPropertyName("athlete")]
        public UFCAthlete Athlete { get; set; } = new();
    }

    public class UFCAthlete
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = "";

        [JsonPropertyName("flag")]
        public UFCFlag Flag { get; set; } = new();
    }

    public class UFCFlag
    {
        [JsonPropertyName("flag")]
        public string CountryName { get; set; } = "";
    }
}