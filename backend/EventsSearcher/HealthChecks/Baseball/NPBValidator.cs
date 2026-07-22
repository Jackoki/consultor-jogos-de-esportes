using System.Text.Json;
using System.Text;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Models.Baseball.NPB;

namespace consultor_jogos_de_esportes.HealthChecks.Baseball
{
    public class NPBValidator : IApiValidator
    {
        private readonly HttpClient _httpClient;
        public string Name => "NPB Beisebol";

        public NPBValidator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ValidationResult> ValidateAsync()
        {
            try
            {
                var body = new
                {
                    date_start = "21-07-2026",
                    date_end = "21-07-2026"
                };

                var json = JsonSerializer.Serialize(body);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://localhost:3000/npb/events", content);

                if (!response.IsSuccessStatusCode)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ApiName = Name,
                        Message = $"HTTP {(int)response.StatusCode}"
                    };
                }

                var responseJson = await response.Content.ReadAsStringAsync();

                var events = JsonSerializer.Deserialize<List<NPBModel>>(responseJson);

                return new ValidationResult
                {
                    IsValid = events != null,
                    ApiName = Name,
                    Message = events != null ? "API funcionando normalmente." : "Resposta inválida."
                };
            }
            catch (Exception ex)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ApiName = Name,
                    Message = ex.Message
                };
            }
        }
    }
}
