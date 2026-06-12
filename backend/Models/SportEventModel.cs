namespace consultor_jogos_de_esportes.Models
{
    public class SportEventModel
    {
        public string SportName {  get; set; }
        public string EventName { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
    }
}
