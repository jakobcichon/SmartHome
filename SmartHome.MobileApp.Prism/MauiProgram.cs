using Example;
using Microsoft.Extensions.Logging;
using SmartHome.Common.Extensions;
using SmartHome.MobileApp.Prism.ViewModels;
using SmartHome.MobileApp.Prism.ViewModels.Devices;
using SmartHome.MobileApp.Prism.ViewModels.Menu;
using SmartHome.MobileApp.Prism.Views;
using SmartHome.MobileApp.Prism.Views.Devices;
using SmartHome.MobileApp.Prism.Views.Menu;
using SmartHome.MobileApp.Services.LocalServerServices;

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

                    container.Register<IServerDiscoveryService, ServerDiscoveryService>();  


                })
                .CreateWindow("NavigationPage/MainPage");

            });
            builder.Configuration.AddCommonConfiguration();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
