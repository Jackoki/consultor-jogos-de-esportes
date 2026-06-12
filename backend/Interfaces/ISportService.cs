using consultor_jogos_de_esportes.DTOs;
using consultor_jogos_de_esportes.Models;

namespace consultor_jogos_de_esportes.Interfaces
{
    public interface ISportService
    {
        Task<List<SportEventModel>> GetEventsAsync(DTOFilterDates filter);
    }
}
