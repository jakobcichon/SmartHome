using Microsoft.Extensions.Logging;
using SmartHome.MobileApp.Prism.ViewModels;
using SmartHome.MobileApp.Prism.ViewModels.Devices;
using SmartHome.MobileApp.Prism.ViewModels.Menu;
using SmartHome.MobileApp.Prism.Views;
using SmartHome.MobileApp.Prism.Views.Devices;
using SmartHome.MobileApp.Prism.Views.Menu;

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
                prism.RegisterTypes(container =>
                {
                    
                    container.RegisterForNavigation<MainPage, MainPageViewModel>();
                    container.RegisterForNavigation<DevicesListView, DevicesListViewModel>();
                    container.RegisterForNavigation<MenuView, MenuViewModel>();
                })
                .CreateWindow("NavigationPage/MainPage");

            });
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
