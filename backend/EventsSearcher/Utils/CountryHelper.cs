using System.Globalization;
using Nager.Country;
using Nager.Country.Translation;

namespace consultor_jogos_de_esportes.Utils
{
    public static class CountryHelper
    {
        private static readonly CountryProvider Provider = new();
        private static readonly TranslationProvider TranslationProvider = new();

        private static readonly Dictionary<string, string> CountryMap = new()
        {
            { "Australia", "Austrália" },
            { "Japan", "Japão" },
            { "China", "China" },
            { "Italy", "Itália" },
            { "United States", "Estados Unidos" },
            { "USA", "Estados Unidos" },
            { "United Kingdom", "Reino Unido" },
            { "Netherlands", "Países Baixos" },
            { "Saudi Arabia", "Arábia Saudita" },
            { "United Arab Emirates", "Emirados Árabes Unidos" },
            { "South Africa", "África do Sul" },
            { "Monaco", "Mônaco" },
            { "Brazil", "Brasil" }
        };

        private static readonly Dictionary<string, string> CountryMapFromAlpha3 = new(StringComparer.OrdinalIgnoreCase)
        {
            { "KSA", "Arábia Saudita" },
            { "ENG", "Inglaterra" },
            { "SCO", "Escócia" },
            { "WAL", "País de Gales" },
            { "UAE", "Emirados Árabes Unidos" },
            { "MNC", "Mônaco" },
            { "NED", "Países Baixos" },
            { "GER", "Alemanha" },
            { "CRO", "Croácia" },
            { "MGL", "Mongólia" },
            { "GRE", "Grécia" },
            { "BOT", "Botsuana" },
            { "RSA", "África do Sul" },
            { "SUI", "Suíça" },
            { "NGR", "Nigéria" },
            { "CRC", "Costa Rica" },
            { "SLO", "Eslováquia" },
            { "POR", "Portugal" },
            { "SRI", "Sri Lanka" },
            { "MAS", "Malásia" },
            { "KOS", "Kosovo" },
            { "IOM", "Ilha de Man" },
            { "ONL", "Online" },
            { "TBD", "A Ser Determinado" },
            { "TBA", "A Ser Anunciado" }
        };

        private static readonly Dictionary<string, string> CountryMapFromAlpha2 = new(StringComparer.OrdinalIgnoreCase)
        {
            { "GB-ENG", "Inglaterra" },
            { "GB-SCT", "Escócia" },
            { "GB-WLS", "País de Gales" },
            { "GB-NIR", "Irlanda do Norte" },
            { "ARAB", "Liga Árabe" },
            { "ASEAN", "ASEAN" },
            { "CEFTA", "CEFTA" },
            { "EAC", "EAC" },
            { "ES-CT", "Catalunha" },
            { "ES-GA", "Galiza" },
            { "ES-PV", "País Basco" },
            { "SH-AC", "Ilha de Ascensão" },
            { "SH-HL", "Santa Helena" },
            { "SH-TA", "Tristão da Cunha" }
        };

        private static readonly Dictionary<string, string> CountryMapAlpha3FromName = new(StringComparer.OrdinalIgnoreCase)
        {
            { "USA", "USA" },
            { "U.S.A", "USA" },
            { "US", "USA" },
            { "U.S", "USA" },
            { "Arábia Saudita", "KSA" },
            { "Inglaterra", "ENG" },
            { "Escócia", "SCO" },
            { "País de Gales", "WAL" },
            { "Emirados Árabes Unidos", "UAE" },
            { "Mônaco", "MNC" },
            { "Países Baixos", "NED" },
            { "Alemanha", "GER" },
            { "Croácia", "CRO" },
            { "Mongólia", "MGL" },
            { "Grécia", "GRE" },
            { "Botsuana", "BOT" },
            { "África do Sul", "RSA" },
            { "Suíça", "SUI" },
            { "Nigéria", "NGR" },
            { "Costa Rica", "CRC" },
            { "Eslováquia", "SLO" },
            { "Portugal", "POR" },
            { "Sri Lanka", "SRI" },
            { "Malásia", "MAS" },
            { "Kosovo", "KOS" },
            { "Ilha de Man", "IOM" },
            { "Online", "ONL" },
            { "A Ser Determinado", "TBD" },
            { "A Ser Anunciado", "TBA" }
        };

        private static readonly Dictionary<string, string> CountryMapAlpha2FromName = new(StringComparer.OrdinalIgnoreCase)
        {
            { "USA", "US" },
            { "U.S.A", "US" },
            { "US", "US" },
            { "U.S", "US" },
            { "Inglaterra", "GB-ENG" },
            { "Escócia", "GB-SCT" },
            { "País de Gales", "GB-WLS" },
            { "Irlanda do Norte", "GB-NIR" },
            { "Liga Árabe", "ARAB" },
            { "ASEAN", "ASEAN" },
            { "CEFTA", "CEFTA" },
            { "EAC", "EAC" },
            { "Catalunha", "ES-CT" },
            { "Galiza", "ES-GA" },
            { "País Basco", "ES-PV" },
            { "Ilha de Ascensão", "SH-AC" },
            { "Santa Helena", "SH-HL" },
            { "Tristão da Cunha", "SH-TA" },
            { "Mônaco", "MC" },
            { "Suíça", "CH" },
            { "England", "GB-ENG" },
            { "Scotland", "GB-SCT" },
            { "Wales", "GB-WLS" },
            { "Northern Ireland", "GB-NIR" }
        };

        public static string? GetCountryNameFromAlpha3(string? countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                return null;

            countryCode = countryCode.Trim().ToUpper();

            string name = "";
            if (CountryMapFromAlpha3.TryGetValue(countryCode, out name))
                return name;

            var country = Provider.GetCountry(countryCode);
            return country?.CommonName ?? countryCode;
        }

        public static string? GetCountryNameFromAlpha2(string? countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                return null;

            countryCode = countryCode.Trim().ToUpper();

            string name = "";
            if (CountryMapFromAlpha2.TryGetValue(countryCode, out name))
                return name;


            var country = Provider.GetCountry(countryCode);

            return country?.CommonName ?? countryCode;
        }

        public static string? GetAlpha3FromCountryName(string? countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return null;

            countryName = countryName.Trim();

            string alpha3 = "";
            if (CountryMapAlpha3FromName.TryGetValue(countryName, out alpha3))
                return alpha3;


            var country = Provider.GetCountryByName(countryName);

            return country?.Alpha3Code.ToString();
        }

        public static string? GetAlpha2FromCountryName(string? countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return null;

            countryName = countryName.Trim();

            string alpha2 = "";
            if (CountryMapAlpha2FromName.TryGetValue(countryName, out alpha2))
                return alpha2;


            var country = Provider.GetCountryByName(countryName);

            return country?.Alpha2Code.ToString();
        }

        public static string GetCountryName(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return string.Empty;

            string translated = "";
            if (CountryMap.TryGetValue(countryName, out translated))
            {
                return translated;
            }

            try
            {
                var country = Provider.GetCountryByName(countryName);
                return TranslationProvider.GetCountryTranslatedName(country.Alpha2Code,LanguageCode.PT) ?? country.CommonName;
            }
            catch
            {
                return countryName;
            }
        }
    }
}