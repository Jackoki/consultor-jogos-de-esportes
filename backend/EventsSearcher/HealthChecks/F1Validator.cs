using consultor_jogos_de_esportes.Models;

namespace consultor_jogos_de_esportes.HealthChecks
{
    public class F1Validator
    {
        private readonly HttpClient _httpClient;

        public string Name = "F1";

        public F1Validator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ValidationResult> ValidateAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<F1Model>>("https://api.openf1.org/v1/meetings");

                if (response == null)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ApiName = Name,
                        Message = "API retornou nulo"
                    };
                }

                if (!response.Any())
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ApiName = Name,
                        Message = "Nenhuma corrida encontrada."
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
