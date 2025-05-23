using Microsoft.Extensions.Options;
using SmartHome.LocalServer.Models.Settings;
using SmartHome.LocalServer.Services;
using SmartHome.LocalServer.Services.DeviceDiscovery;

namespace SmartHome.LocalServer.Extensions.Services.DeviceDiscovery;

public static class LocalDeviceDiscoveryServiceExtensions
{
    public static IServiceCollection AdLocalDeviceDiscoveryServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IDeviceDiscoveryService>(services =>
        {
            var options = services.GetRequiredService<IOptions<SmartHomeSettingsModel>>().Value;
            return new UdpDeviceDiscoveryService(options.ServerUdpPort, options.ClientUdpPort);
        });
        
        return serviceCollection;
    }
}