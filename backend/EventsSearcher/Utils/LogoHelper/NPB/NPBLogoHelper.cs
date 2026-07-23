namespace consultor_jogos_de_esportes.Utils.LogoHelper.NPB
{
    public static class NPBLogoHelper
    {
        private static readonly Dictionary<string, string> Logos = new()
        {
            ["S"] = "/imgs/npb/yakult_swallows.png",
            ["G"] = "/imgs/npb/tokyo_giants.png",
            ["T"] = "/imgs/npb/hanshin_tigers.png",
            ["D"] = "/imgs/npb/chuniji_dragons.png",
            ["F"] = "/imgs/npb/hokkaido_fighters.png",
            ["B"] = "/imgs/npb/orix_buffaloes.png",
            ["E"] = "/imgs/npb/hakuten_eagles.png",
            ["M"] = "/imgs/npb/chiba_marines.png",
            ["H"] = "/imgs/npb/softbanks_hawks.png",
            ["L"] = "/imgs/npb/saitama_seibu.png",
            ["DB"] = "/imgs/npb/baystars_yokohama.png",
            ["C"] = "/imgs/npb/hiroshima_carp.png"
        };

        public static string? GetLogo(string? code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;

            var logo = "";
            return Logos.TryGetValue(code.Trim().ToUpper(), out logo) ? logo : null;
        }
    }
}
