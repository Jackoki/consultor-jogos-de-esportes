using System.Text.Json.Serialization;

namespace consultor_jogos_de_esportes.Models
{
    public class ChessModel
    {
        [JsonPropertyName("name")]
        public string NameEvent { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("date_start")]
        public DateTime DateTimeStart { get; set; }

        [JsonPropertyName("date_end")]
        public DateTime DateTimeEnd { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("country_code_alpha2")]
        public string CountryCodeAlpha2 { get; set; }

    }
}
