using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.Baseball
{
    public class MLBResponse
    {
        [JsonPropertyName("dates")]
        public List<MLBDate> Dates { get; set; } = new();
    }

    public class MLBDate
    {
        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("games")]
        public List<MLBModel> Games { get; set; } = new();
    }
}