using System.Text.Json;
using consultor_jogos_de_esportes.Models;
using consultor_jogos_de_esportes.Models.Basketball;

namespace consultor_jogos_de_esportes.HealthChecks.Basketball
{
    public class NBAValidator : IApiValidator
    {
        private readonly HttpClient _httpClient;
        public string Name => "NBA";

        public NBAValidator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ValidationResult> ValidateAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://site.api.espn.com/apis/site/v2/sports/basketball/nba/scoreboard?dates=20260603");

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

                var result = JsonSerializer.Deserialize<NBAResponse>(responseJson);

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