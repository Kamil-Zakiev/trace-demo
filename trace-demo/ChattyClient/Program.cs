using System.Diagnostics;

var httpClient = new HttpClient()
{
    BaseAddress = new Uri("http://localhost:5090")
};

const int rps = 10;
const int rounds = 10;
var tasks = new List<Task<long>>(rps * rounds);
for (var i = 0; i < rps * rounds; i++)
{
    var task = Task.Run(async () =>
    {
        var stopwatch = Stopwatch.StartNew();
        await httpClient.GetAsync("");
        return stopwatch.ElapsedMilliseconds;
    });
    tasks.Add(task);
    
    if (i % rps == 0)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
    }
}

var timings = await Task.WhenAll(tasks);
var maxTimings = timings.OrderByDescending(x => x).Take(5);
Console.WriteLine($"Max timings (ms): {string.Join(", ", maxTimings)}");