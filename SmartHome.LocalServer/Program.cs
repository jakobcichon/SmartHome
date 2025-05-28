using SmartHome.LocalServer.Extensions.Services.DeviceDiscovery;
using SmartHome.LocalServer.Services;
using SmartHome.LocalServer.Services.DeviceDiscovery;
using SmartHome.Common.Extensions;
using SmartHome.Common.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Configuration.AddCommonConfiguration();
builder.Services.Configure<SmartHomeCommonSettingsModel>(builder.Configuration.GetSection("SmartHomeSettings"));

builder.Services.AddHostedService<DeviceDiscoveryService>();
builder.Services.AdLocalDeviceDiscoveryServices();

var app = builder.Build();

app.MapGrpcService<HeatPumpService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();