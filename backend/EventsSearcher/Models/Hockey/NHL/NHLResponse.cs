using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.Hockey;

public class NHLResponse
{
    [JsonPropertyName("gameWeek")]
    public List<NHLGameWeek> GameWeek { get; set; } = [];
}

public class NHLGameWeek
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("games")]
    public List<NHLGame> Games { get; set; } = [];
}

public class NHLGame
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("startTimeUTC")]
    public DateTime StartTimeUTC { get; set; }

    [JsonPropertyName("venue")]
    public NHLVenue Venue { get; set; } = new();

    [JsonPropertyName("awayTeam")]
    public NHLTeam AwayTeam { get; set; } = new();

    [JsonPropertyName("homeTeam")]
    public NHLTeam HomeTeam { get; set; } = new();

    public DateTime DateTimeStart => StartTimeUTC;

    public DateTime DateTimeEnd => StartTimeUTC.AddHours(3);

    public string NameEvent => $"{HomeTeam.PlaceName.Default} {HomeTeam.CommonName.Default} vs " + $"{AwayTeam.PlaceName.Default} {AwayTeam.CommonName.Default}";

    public string CountryName => "USA";
}

public class NHLVenue
{
    [JsonPropertyName("default")]
    public string Name { get; set; } = string.Empty;
}

public class NHLTeam
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("abbrev")]
    public string Abbrev { get; set; } = string.Empty; 
    
    [JsonPropertyName("logo")]
    public string Logo { get; set; } = string.Empty;

    [JsonPropertyName("commonName")]
    public NHLLocalizedName CommonName { get; set; } = new();

    [JsonPropertyName("placeName")]
    public NHLLocalizedName PlaceName { get; set; } = new();
}

public class NHLLocalizedName
{
    [JsonPropertyName("default")]
    public string Default { get; set; } = string.Empty;
}