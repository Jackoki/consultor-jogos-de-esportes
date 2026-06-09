using consultor_jogos_de_esportes.Utils;

namespace consultor_jogos_de_esportes.DTOs
{
    public class DTOFilterDates
    {
        public DateFilterType dateFilterType { get; set; }

        public DateTime? Date {  get; set; }
    }
}
