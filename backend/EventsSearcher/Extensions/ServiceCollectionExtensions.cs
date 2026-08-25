using consultor_jogos_de_esportes.HealthChecks;
using consultor_jogos_de_esportes.HealthChecks.AmericanFootball;
using consultor_jogos_de_esportes.HealthChecks.Baseball;
using consultor_jogos_de_esportes.HealthChecks.Basketball;
using consultor_jogos_de_esportes.HealthChecks.Chess;
using consultor_jogos_de_esportes.HealthChecks.F1;
using consultor_jogos_de_esportes.HealthChecks.Fighting;
using consultor_jogos_de_esportes.HealthChecks.Hockey;
using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Services;
using consultor_jogos_de_esportes.Services.AmericanFootball;
using consultor_jogos_de_esportes.Services.Baseball;
using consultor_jogos_de_esportes.Services.Basketball;
using consultor_jogos_de_esportes.Services.Fighting;
using consultor_jogos_de_esportes.Services.Hockey;
using consultor_jogos_de_esportes.Services.Motorsport;
using consultor_jogos_de_esportes.Services.Tennis;

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
        services.AddHttpClient<NHLService>();
        services.AddHttpClient<UFCService>();
        services.AddHttpClient<ATPService>();
        services.AddHttpClient<WWEService>();

        services.AddScoped<MotorsportService>();
        services.AddScoped<ChessService>();
        services.AddScoped<BaseballService>();
        services.AddScoped<BasketballService>();
        services.AddScoped<AmericanFootballService>();
        services.AddScoped<FightingService>();
        services.AddScoped<HockeyService>();
        services.AddScoped<TennisService>();
        services.AddScoped<WrestlingService>();

        services.AddScoped<ISportService, MotorsportService>();
        services.AddScoped<ISportService, ChessService>();
        services.AddScoped<ISportService, BaseballService>();
        services.AddScoped<ISportService, BasketballService>();
        services.AddScoped<ISportService, AmericanFootballService>();
        services.AddScoped<ISportService, FightingService>();
        services.AddScoped<ISportService, HockeyService>();
        services.AddScoped<ISportService, TennisService>();
        services.AddScoped<ISportService, WrestlingService>();

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
        services.AddScoped<IApiValidator, NCAAValidator>();

        services.AddHttpClient<NPBValidator>();
        services.AddScoped<IApiValidator, NPBValidator>();

        services.AddHttpClient<MLBValidator>();
        services.AddScoped<IApiValidator, MLBValidator>();

        services.AddHttpClient<NFLValidator>();
        services.AddScoped<IApiValidator, NFLValidator>();

        services.AddHttpClient<UFCValidator>();
        services.AddScoped<IApiValidator, UFCValidator>();

        services.AddHttpClient<NHLValidator>();
        services.AddScoped<IApiValidator, NHLValidator>();

        services.AddScoped<ApiHealthManager>();

        return services;
    }
}