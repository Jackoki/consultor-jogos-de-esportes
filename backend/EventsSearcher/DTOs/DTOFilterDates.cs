using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.DTOs
{
    public class DTOFilterDates
    {
        public DateFilterType DateFilterType { get; set; }

        public DateTime? Date {  get; set; }
        public string? Sport { get; set; }
        public string? League { get; set; }
    }
}
