using Microsoft.Extensions.Logging;
using SmartHome.Common.Extensions;
using SmartHome.MobileApp.Prism.Extensions.Maui;
using SmartHome.MobileApp.Prism.Extensions.Prism;

namespace SmartHome.MobileApp.Prism
{
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
            builder.UsePrism(new DryIocContainerExtension(), prism =>
            {
                prism.Configure()
                .CreateWindow("NavigationPage/MainPage");

            });
            builder.Configuration.AddCommonConfiguration();
            builder.BindConfigurationToModel();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
