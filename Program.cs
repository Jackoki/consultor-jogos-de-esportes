using consultor_jogos_de_esportes.Services;

var client = new HttpClient();
var f1Service = new F1Service(client);

var f1Events = await f1Service.GetMeetingsAsync(2026);

foreach (var f1Event in f1Events)
{
    Console.WriteLine($"{f1Event.NameEvent}");
}

Console.ReadLine();