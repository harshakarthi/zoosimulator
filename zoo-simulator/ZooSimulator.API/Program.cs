using ZooSimulator.API.Services;
using ZooSimulator.Core.Configuration;
using ZooSimulator.Core.Services.Implementation;
using ZooSimulator.Core.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration.GetSection("Zoo").Get<ZooConfiguration>() ?? new ZooConfiguration();
builder.Services.AddSingleton(config);

builder.Services.AddSingleton<IRandomProvider, RandomProvider>();
builder.Services.AddSingleton<IZooService, ZooService>();

builder.Services.AddSingleton<ZooSimulationService>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<ZooSimulationService>());

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();