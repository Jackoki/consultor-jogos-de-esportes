using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.AmericanFootball
{
    public class NFLResponse
    {
        [JsonPropertyName("events")]
        public List<NFLModel> Events { get; set; } = new();
    }
}