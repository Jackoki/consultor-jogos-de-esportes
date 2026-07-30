using consultor_jogos_de_esportes.HealthChecks;
using consultor_jogos_de_esportes.HealthChecks.Baseball;
using consultor_jogos_de_esportes.HealthChecks.Basketball;
using consultor_jogos_de_esportes.HealthChecks.Chess;
using consultor_jogos_de_esportes.HealthChecks.F1;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Services;
using consultor_jogos_de_esportes.Services.Baseball;
using consultor_jogos_de_esportes.Services.Basketball;
using consultor_jogos_de_esportes.Services.Fighting;
using consultor_jogos_de_esportes.Services.Motorsport;

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
        services.AddHttpClient<NFLService>();
        services.AddHttpClient<UFCService>();

        services.AddScoped<MotorsportService>();
        services.AddScoped<ChessService>();
        services.AddScoped<BaseballService>();
        services.AddScoped<BasketballService>();
        services.AddScoped<AmericanFootballService>();
        services.AddScoped<FightingService>();

        services.AddScoped<ISportService, MotorsportService>();
        services.AddScoped<ISportService, ChessService>();
        services.AddScoped<ISportService, BaseballService>();
        services.AddScoped<ISportService, BasketballService>();
        services.AddScoped<ISportService, AmericanFootballService>();
        services.AddScoped<ISportService, FightingService>();

        return services;
    }

    public static IServiceCollection AddHealthChecksCustom(this IServiceCollection services)
    {
        services.AddHttpClient<F1Validator>();
        IServiceCollection serviceCollection = services.AddScoped<IApiValidator, F1Validator>();

        services.AddHttpClient<ChessValidator>();
        services.AddScoped<IApiValidator, ChessValidator>();

        services.AddHttpClient<NBAValidator>();
        services.AddScoped<IApiValidator, NBAValidator>();

        services.AddHttpClient<NCAAValidator>();
        services.AddHttpClient<NPBValidator>();
        services.AddHttpClient<MLBValidator>();
        services.AddScoped<IApiValidator, NCAAValidator>();
        services.AddScoped<IApiValidator, NPBValidator>();
        services.AddScoped<IApiValidator, MLBValidator>();

        services.AddScoped<ApiHealthManager>();

        return services;
    }
}