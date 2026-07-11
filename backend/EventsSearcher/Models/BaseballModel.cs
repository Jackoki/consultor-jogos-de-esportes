using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models
{
    public class BaseballModel
    {
        [JsonPropertyName("meeting_key")]
        public int Id { get; set; }

        [JsonPropertyName("meeting_official_name")]
        public string NameEvent { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_flag")]
        public string CountryFlag { get; set; }

        [JsonPropertyName("circuit_image")]
        public string CircuitImage { get; set; }

        [JsonPropertyName("date_start")]
        public DateTime DateTimeStart { get; set; }

        [JsonPropertyName("date_end")]
        public DateTime DateTimeEnd { get; set; }

        [JsonPropertyName("is_cancelled")]
        public bool IsCancelled { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

    }
}
