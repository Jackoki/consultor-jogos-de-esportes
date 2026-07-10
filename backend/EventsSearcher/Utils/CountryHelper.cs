using System.Globalization;

namespace consultor_jogos_de_esportes.Utils
{
    public static class CountryHelper
    {
        private static readonly Dictionary<string, string> CountryMap = new()
        {
            { "Australia", "Austrália" },
            { "Japan", "Japão" },
            { "China", "China" },
            { "Italy", "Itália" },
            { "United States", "Estados Unidos" },
            { "United Kingdom", "Reino Unido" },
            { "Netherlands", "Países Baixos" },
            { "Saudi Arabia", "Arábia Saudita" },
            { "United Arab Emirates", "Emirados Árabes Unidos" },
            { "South Africa", "África do Sul" },
            { "Monaco", "Mônaco" },
            { "Brazil", "Brasil" }
        };

        public static string GetCountryName(string? countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return string.Empty;

            return CountryMap.TryGetValue(countryName, out var translated) ? translated : countryName;
        }
    }
}