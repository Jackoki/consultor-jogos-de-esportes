using consultor_jogos_de_esportes.HealthChecks;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Services;
using consultor_jogos_de_esportes.Services.Baseball;
using consultor_jogos_de_esportes.Services.Basketball;

namespace consultor_jogos_de_esportes.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSportServices(this IServiceCollection services)
    {
        services.AddHttpClient<F1Service>();
        services.AddHttpClient<ChessService>();
        services.AddHttpClient<MLBService>();
        services.AddHttpClient<NCAAService>();
        services.AddHttpClient<NPBService>();
        services.AddHttpClient<NBAService>();

        services.AddScoped<F1Service>();
        services.AddScoped<ChessService>();
        services.AddScoped<BaseballService>();
        services.AddScoped<BasketballService>();

        services.AddScoped<ISportService, F1Service>();
        services.AddScoped<ISportService, ChessService>();
        services.AddScoped<ISportService, BaseballService>();
        services.AddScoped<ISportService, BasketballService>();

        return services;
    }

    public static IServiceCollection AddHealthChecksCustom(this IServiceCollection services)
    {
        services.AddHttpClient<F1Validator>();
        IServiceCollection serviceCollection = services.AddScoped<IApiValidator, F1Validator>();

        services.AddScoped<ApiHealthManager>();

        return services;
    }
}