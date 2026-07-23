using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.Baseball.NPB
{
    public class NPBModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("homeTeam")]
        public NPBTeam HomeTeam { get; set; } = new();

        [JsonPropertyName("awayTeam")]
        public NPBTeam AwayTeam { get; set; } = new();
    }

    public class NPBTeam
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
