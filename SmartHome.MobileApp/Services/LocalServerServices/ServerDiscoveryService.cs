using Microsoft.Extensions.Options;
using SmartHome.Common.Extensions.String;
using SmartHome.Common.Models.Settings;
using SmartHomeClientApp.Services.LocalServerServices.CommunicationInterfaces;
using SmartHomeClientApp.Services.LocalServerServices.Models;

namespace SmartHomeClientApp.Services.LocalServerServices;

public class ServerDiscoveryService(IServerDiscoveryInterface _commInterface, 
    IOptions<SmartHomeCommonSettingsModel> _options ) : IServerDiscoveryService
{
    public async Task<LocalServerModel?> GetFirstAvailableServerAsync(
        CancellationToken stoppingToken, TimeSpan timeout)
    {
        var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken, 
            new CancellationTokenSource(timeout).Token);
        
        await _commInterface.SendRequestAsync(_options.Value.LocalDeviceCall.ToUtf8(), linkedTokenSource.Token);

        while (!linkedTokenSource.Token.IsCancellationRequested)
        {
            var response = (await _commInterface.ReceiveDataAsync(linkedTokenSource.Token)).ToString() ?? 
                           string.Empty;

            if (!response.Contains(_options.Value.ServerCallResponse)) continue;
            if (LocalServerModel.TryParse(response, null, out var serverModel)) return serverModel;
            
            break;
        }
        
        return null;
    }
}