using System.Text.Json.Serialization;

public class NCAATeamModel
{
    [JsonPropertyName("names")]
    public NCAATeamNamesModel Names { get; set; } = new();

    [JsonIgnore]
    public string Name => Names.Short;
}