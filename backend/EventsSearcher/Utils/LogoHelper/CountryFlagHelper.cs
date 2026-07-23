namespace consultor_jogos_de_esportes.Utils.LogoHelper
{
    public static class CountryFlagHelper
    {
        public static string GetCountryFlag(string alpha2)
        {
            if (string.IsNullOrWhiteSpace(alpha2))
                return string.Empty;

            return $"/imgs/flags/{alpha2.ToLower()}.svg";
        }
    }
}
