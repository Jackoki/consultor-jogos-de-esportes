using System.Text.Json;
using consultor_jogos_de_esportes.Models.Hockey;

namespace consultor_jogos_de_esportes.HealthChecks.Hockey
{
    public class NHLValidator : IApiValidator
    {
        private readonly HttpClient _httpClient;
        public string Name => "NHL";

        public NHLValidator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ValidationResult> ValidateAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://api-web.nhle.com/v1/schedule/2026-09-19");

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

                var result = JsonSerializer.Deserialize<NHLResponse>(responseJson);

                if (result == null)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ApiName = Name,
                        Message = "Resposta inválida."
                    };
                }

                return new ValidationResult
                {
                    IsValid = true,
                    ApiName = Name,
                    Message = "API funcionando normalmente."
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