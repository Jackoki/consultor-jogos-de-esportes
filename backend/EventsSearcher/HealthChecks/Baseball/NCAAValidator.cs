using System.Text.Json;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Models.Baseball;
using consultor_jogos_de_esportes.Models.Basketball;

namespace consultor_jogos_de_esportes.HealthChecks.Baseball
{
    public class NCAAValidator : IApiValidator
    {
        private readonly HttpClient _httpClient;
        public string Name => "NCAA Beisebol";

        public NCAAValidator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ValidationResult> ValidateAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://ncaa-api.henrygd.me/scoreboard/baseball/d1/2026/07/21/all-conf");

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

                var result = JsonSerializer.Deserialize<MLBResponse>(responseJson);

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