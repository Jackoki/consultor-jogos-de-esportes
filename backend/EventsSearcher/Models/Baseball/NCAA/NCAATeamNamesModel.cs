using System.Text.Json.Serialization;

public class NCAATeamNamesModel
{
    [JsonPropertyName("short")]
    public string Short { get; set; } = "";
}