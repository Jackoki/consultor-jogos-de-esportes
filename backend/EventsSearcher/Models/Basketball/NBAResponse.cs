using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.Basketball
{
    public class NBAResponse
    {
        [JsonPropertyName("events")]
        public List<NBAModel> Events { get; set; } = new();
    }
}