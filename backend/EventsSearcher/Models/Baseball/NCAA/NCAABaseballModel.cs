using System.Text.Json.Serialization;

public class NCAABaseballModel
{
    [JsonPropertyName("title")]
    public string NameEvent { get; set; } = "";

    [JsonPropertyName("startTimeEpoch")]
    public string StartTimeEpoch { get; set; }

    [JsonPropertyName("home")]
    public NCAATeamModel Home { get; set; } = new();

    [JsonPropertyName("away")]
    public NCAATeamModel Away { get; set; } = new();

    [JsonIgnore]
    public DateTime DateTimeStart => DateTimeOffset.FromUnixTimeSeconds(long.Parse(StartTimeEpoch)).UtcDateTime;

    [JsonIgnore]
    public DateTime DateTimeEnd => DateTimeStart.AddHours(3);

    [JsonIgnore]
    public string HomeTeam => Home.Name;

    [JsonIgnore]
    public string AwayTeam => Away.Name;
}