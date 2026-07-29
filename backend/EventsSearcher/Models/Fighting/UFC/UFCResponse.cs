using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.Fighting
{
    public class UFCResponse
    {
        [JsonPropertyName("events")]
        public List<UFCModel> Events { get; set; } = new();
    }
}