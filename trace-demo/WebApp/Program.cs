var random = new Random();
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    var delayMs = random.Next(500, 1000);
    Task.Delay(TimeSpan.FromMilliseconds(delayMs)).Wait();
    return "Hello World!";
});

app.Run();