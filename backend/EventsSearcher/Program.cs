using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Services;
using consultor_jogos_de_esportes.Services.Baseball;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<F1Service>();
builder.Services.AddHttpClient<ChessService>();
builder.Services.AddHttpClient<MLBService>();
builder.Services.AddHttpClient<NCAAService>();
builder.Services.AddHttpClient<BaseballService>();

builder.Services.AddScoped<ISportService>(sp =>sp.GetRequiredService<F1Service>());
builder.Services.AddScoped<ISportService>(sp => sp.GetRequiredService<ChessService>()); 
builder.Services.AddScoped<ISportService>(sp => sp.GetRequiredService<BaseballService>());

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("ReactPolicy");

app.MapControllers();

app.Run();