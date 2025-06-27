using StackExchange.Redis;
using UrlShortener.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(opt => 
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("DockerRedisConnection")));

builder.Services.AddScoped<IUrlRepo, RedisUrlRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
