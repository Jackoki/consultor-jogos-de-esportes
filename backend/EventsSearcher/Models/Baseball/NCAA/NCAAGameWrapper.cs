using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.DTOs.NCAA
{
    public class NCAAGameWrapper
    {
        [JsonPropertyName("game")]
        public NCAABaseballModel Game { get; set; } = new();
    }
}}