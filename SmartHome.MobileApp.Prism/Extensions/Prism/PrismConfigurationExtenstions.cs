using Microsoft.Extensions.Options;
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

namespace SmartHome.MobileApp.Prism.Extensions.Prism
{
    internal static class PrismConfigurationExtenstions
    {
        public static PrismAppBuilder Configure(this PrismAppBuilder builder)
        {
            builder.RegisterTypes(registry =>
            {
                registry.RegisterViews();
                registry.RegisterServices();

            });
            return builder;
        }

        public static IContainerRegistry RegisterViews(this IContainerRegistry registry)
        {
            registry.RegisterForNavigation<MainPage, MainPageViewModel>();
            registry.RegisterForNavigation<DevicesListView, DevicesListViewModel>();
            registry.RegisterForNavigation<MenuView, MenuViewModel>();

            return registry;
        }

        public static IContainerRegistry RegisterServices(this IContainerRegistry registry)
        {
            registry.Register<IDiscoveryInterface>(provider =>
            {            
                var commonSettingsModel = provider.Resolve<IOptions<SmartHomeCommonSettingsModel>>().Value;
                var serverPort = commonSettingsModel.ServerUdpPort;
                var clientPort = commonSettingsModel.ClientUdpPort;

                return new UdpServerDiscoveryInterface(serverPort, clientPort);
            });

            registry.Register<IServerDiscoveryService>(provider =>
            {
                return new ServerDiscoveryService(provider.Resolve<IDiscoveryInterface>(),
                    provider.Resolve<IOptions<SmartHomeCommonSettingsModel>>());
            });

            return registry;
        }
    }
}
