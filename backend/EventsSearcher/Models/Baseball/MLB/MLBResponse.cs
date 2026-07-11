namespace consultor_jogos_de_esportes.Models.Baseball
{
    public class MLBResponse
    {
        public List<MLBDate> Dates { get; set; } = new();
    }

    public class MLBDate
    {
        public string Date { get; set; } = string.Empty;

        public List<MLBModel> Games { get; set; } = new();
    }
}