using consultor_jogos_de_esportes.Interfaces;
using consultor_jogos_de_esportes.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<F1Service>();
builder.Services.AddHttpClient<ChessService>();

builder.Services.AddScoped<ISportService>(sp =>sp.GetRequiredService<F1Service>());
builder.Services.AddScoped<ISportService>(sp => sp.GetRequiredService<ChessService>());

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