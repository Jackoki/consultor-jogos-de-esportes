namespace consultor_jogos_de_esportes.HealthChecks
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }

        public string Message { get; set; } = string.Empty;

        public string ApiName { get; set; } = string.Empty;
    }
}
