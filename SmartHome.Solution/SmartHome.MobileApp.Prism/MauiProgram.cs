using Microsoft.Extensions.Logging;

namespace SmartHome.MobileApp.Prism;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.UsePrism(prism =>
        {
            ;
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}