using Microsoft.Extensions.Options;
using SmartHome.Common.Extensions.String;
using SmartHome.Common.Models.Settings;
using SmartHomeClientApp.Services.LocalServerServices.CommunicationInterfaces;
using SmartHomeClientApp.Services.LocalServerServices.Models;

namespace SmartHomeClientApp.Services.LocalServerServices;

public class ServerDiscoveryService(IServerDiscoveryInterface _commInterface, IOptions<SmartHomeCommonSettingsModel> _options) : IServerDiscoveryService
{
    public async Task<LocalServerModel> IServerDiscoveryService.GetFirstAvailableServerAsync(TimeSpan timeout, CancellationToken stoppingToken)
    {
        await _commInterface.SendRequestAsync(_options.Value.LocalDeviceCall.ToUtf8(), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await _commInterface.ReceiveDataAsync(stoppingToken);
        }
    }
}