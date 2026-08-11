using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.Tennis
{
    public class ATPResponse
    {
        [JsonPropertyName("events")]
        public List<ATPModel> Events { get; set; } = new();
    }
}