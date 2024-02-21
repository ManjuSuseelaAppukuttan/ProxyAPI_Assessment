using APIInterfaces;
using APIServices;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueLimit = 0;
    });
    // Changing the status code
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});


IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

builder.Services.AddSingleton<IConfiguration>(config);
builder.Services.AddHttpClient();
builder.Services.AddTransient(typeof(IStarWarsAPIService<>), typeof(StarWarsAPIService<>));


string baseuri = config.GetValue<string>("StarWarsBaseUrl:baseUri");
builder.Services.AddHttpClient("starWarsClient", httpClient =>
{
    httpClient.BaseAddress = new Uri(baseuri);
    httpClient.Timeout = new TimeSpan(0, 0, 30);
    httpClient.DefaultRequestHeaders.Clear();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowOrigin");
app.UseRateLimiter();


static string GetTicks() => (DateTime.Now.Ticks & 0x11111).ToString("00000");

app.MapGet("/", () => Results.Ok($"Hello {GetTicks()}"))
                           .RequireRateLimiting("fixed");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();