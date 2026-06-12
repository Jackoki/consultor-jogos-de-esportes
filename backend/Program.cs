using consultor_jogos_de_esportes.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient<F1Service>();

var app = builder.Build();
app.MapControllers();
app.Run();