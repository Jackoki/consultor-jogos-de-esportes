using System.Text.Json.Serialization;
using consultor_jogos_de_esportes.Models.Baseball;

namespace consultor_jogos_de_esportes.Models.Basketball
{
    public class NBAResponse
    {
        [JsonPropertyName("events")]
        public List<NBAModel> Events { get; set; } = new();
    }
}