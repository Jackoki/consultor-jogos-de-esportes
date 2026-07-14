using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models.Baseball.NPB
{
    public class NPBModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
