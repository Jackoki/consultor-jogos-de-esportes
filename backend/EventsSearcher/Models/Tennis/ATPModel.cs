using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.Tennis
{
    public class ATPModel
    {
        [JsonPropertyName("groupings")]
        public List<ATPGrouping> Groupings { get; set; } = new();

        [JsonPropertyName("shortName")]
        public string EventName { get; set; } = "";
    }

    public class ATPGrouping
    {
        [JsonPropertyName("grouping")]
        public ATPGroupingInformation Grouping { get; set; } = new();

        [JsonPropertyName("competitions")]
        public List<ATPCompetition> Competitions { get; set; } = new();
    }

    public class ATPGroupingInformation
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = "";
    }

    public class ATPCompetition
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("venue")]
        public ATPVenue Venue { get; set; } = new(); 

        [JsonPropertyName("round")]
        public ATPRound Round { get; set; } = new();

        [JsonPropertyName("competitors")]
        public List<ATPCompetitor> Competitors { get; set; } = new();
    }

    public class ATPVenue
    {
        [JsonPropertyName("fullName")]
        public string Name { get; set; } = "";
    }
    public class ATPRound
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = "";
    }
    
    public class ATPCompetitor
    {
        [JsonPropertyName("athlete")]
        public ATPAthlete Athlete { get; set; } = new();
    }

    public class ATPAthlete
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = ""; 

        [JsonPropertyName("flag")]
        public ATPFlag Flag { get; set; } = new();
    }

    public class ATPFlag
    {
        [JsonPropertyName("alt")]
        public string FlagName { get; set; } = "";
    }
}