using Microsoft.Extensions.Options;
using SmartHome.Common.Models.Settings;
using SmartHome.Common.Services.CommunicationInterfaces;
using SmartHome.LocalServer.Services.DeviceDiscovery.CommunicationInterfaces;

namespace SmartHome.LocalServer.Extensions.Services.DeviceDiscovery;

public static class LocalDeviceDiscoveryServiceExtensions
{
    public static IServiceCollection AdLocalDeviceDiscoveryServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IDiscoveryInterface>(services =>
        {
            var options = services.GetRequiredService<IOptions<SmartHomeCommonSettingsModel>>().Value;
            return new UdpDeviceDiscoveryInterface(options.ServerUdpPort, options.ClientUdpPort);
        });
        
        return serviceCollection;
    }
}