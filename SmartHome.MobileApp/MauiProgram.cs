﻿global using Grpc.Net.Client;
using System.Reflection;
using Microsoft.Extensions.Logging;
using SmartHome.Protos;
using SmartHome.Common.Extensions;

namespace SmartHome.MobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });
        builder.Configuration.AddCommonConfiguration();

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddScoped(services =>
        {
            return new HeatPump.HeatPumpClient(GrpcChannel.ForAddress("https://localhost:7234"));
        });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        
        return builder.Build();
    }
}
