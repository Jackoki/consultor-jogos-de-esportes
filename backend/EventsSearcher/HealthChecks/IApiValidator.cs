namespace consultor_jogos_de_esportes.HealthChecks
{
    public interface IApiValidator
    {
        string Name { get; }

        Task<ValidationResult> ValidateAsync();
    }
}
