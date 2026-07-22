namespace consultor_jogos_de_esportes.Models
{
    public class SportEventModel
    {
        public string SportName {  get; set; }
        public string EventName { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public bool HasTime { get; set; }
        public string? CentralImage { get; set; }
        public string? LeftImage { get; set; }
        public string? RightImage { get; set; }
    }
}
