using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models
{
    public class WrestlingModel
    {
        [JsonPropertyName("name")]
        public string NameEvent { get; set; }

        [JsonPropertyName("image")]
        public string CentralImage { get; set; }

        [JsonPropertyName("date_start")]
        public DateTime DateTimeStart { get; set; }

        [JsonPropertyName("date_end")]
        public DateTime DateTimeEnd { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }
    }
}
