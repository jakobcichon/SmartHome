using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartHome.Common.Extensions;
using SmartHome.Common.Models.Settings;
using SmartHome.Common.Services.CommunicationInterfaces;
using SmartHome.MobileApp.Prism.ViewModels;
using SmartHome.MobileApp.Prism.ViewModels.Devices;
using SmartHome.MobileApp.Prism.ViewModels.Menu;
using SmartHome.MobileApp.Prism.Views;
using SmartHome.MobileApp.Prism.Views.Devices;
using SmartHome.MobileApp.Prism.Views.Menu;
using SmartHome.MobileApp.Services.LocalServerServices;
using SmartHome.MobileApp.Services.LocalServerServices.CommunicationInterfaces;

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

                    container.Register<IDiscoveryInterface>(c =>
                    {
                        var commonSettingsModel = c.Resolve<IOptions<SmartHomeCommonSettingsModel>>().Value;
                        var serverPort = commonSettingsModel.ServerUdpPort;
                        var clientPort = commonSettingsModel.ClientUdpPort;

                        return new UdpServerDiscoveryInterface(serverPort, clientPort);
                    });

                    container.Register<IServerDiscoveryService>(c =>
                    {
                        return new ServerDiscoveryService(c.Resolve<IDiscoveryInterface>(),
                            c.Resolve<IOptions<SmartHomeCommonSettingsModel>>());
                    });
                })
                .CreateWindow("NavigationPage/MainPage");

            });
            builder.Configuration.AddCommonConfiguration();
            builder.Services.Configure<SmartHomeCommonSettingsModel>(builder.Configuration.GetSection("SmartHomeSettings"));

    
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
