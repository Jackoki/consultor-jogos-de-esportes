using System.Text.Json;
using consultor_jogos_de_esportes.Models;

namespace consultor_jogos_de_esportes.Services
{
    public class F1Service
    {
        private readonly HttpClient _httpClient;

        public F1Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<F1Model>> GetMeetingsAsync(int year)
        {
            var response = await _httpClient.GetAsync($"https://api.openf1.org/v1/meetings?year={year}");

            if(!response.IsSuccessStatusCode)
                return new List<F1Model>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<F1Model>>(json);
        }
    }
}
