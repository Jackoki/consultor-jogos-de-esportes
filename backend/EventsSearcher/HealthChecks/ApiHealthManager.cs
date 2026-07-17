namespace consultor_jogos_de_esportes.HealthChecks
{
    public class ApiHealthManager
    {
        private readonly IEnumerable<IApiValidator> _validators;

        public ApiHealthManager(IEnumerable<IApiValidator> validators)
        {
            _validators = validators;
        }

        public async Task<List<ValidationResult>> ValidateAllAsync()
        {
            var results = new List<ValidationResult>();

            foreach (var validator in _validators)
            {
                results.Add(await validator.ValidateAsync());
            }

            return results;
        }
    }
}
