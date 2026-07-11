using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.DTOs.NCAA
{
    public class NCAAScoreboardResponse
    {
        [JsonPropertyName("games")]
        public List<NCAAGameWrapper> Games { get; set; } = [];
    }
}