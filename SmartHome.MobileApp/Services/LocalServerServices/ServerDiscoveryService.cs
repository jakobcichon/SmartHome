using Microsoft.Extensions.Options;
using SmartHome.Common.Extensions.String;
using SmartHome.Common.Models.Settings;
using SmartHome.Common.Services.CommunicationInterfaces;
using System.Text;
using SmartHome.MobileApp.Models;

namespace SmartHome.MobileApp.Services.LocalServerServices;

public class ServerDiscoveryService(IDiscoveryInterface _commInterface, 
    IOptions<SmartHomeCommonSettingsModel> _options ) : IServerDiscoveryService
{
    public async Task<LocalServerModel?> GetFirstAvailableServerAsync(
        CancellationToken stoppingToken, TimeSpan timeout)
    {
        CancellationTokenSource linkedTokenSource = GetLinkedToken(timeout, stoppingToken);

        await _commInterface.SendRequestAsync(_options.Value.LocalDeviceCall.ToUtf8(), linkedTokenSource.Token);

        while (!linkedTokenSource.Token.IsCancellationRequested)
        {
            var rawResponse = await _commInterface.ReceiveDataAsync(linkedTokenSource.Token);
            var response = Encoding.UTF8.GetString(rawResponse);
            if (!response.Contains(_options.Value.ServerCallResponse)) continue;
            if (LocalServerModel.TryParse(response, null, out var serverModel)) return serverModel;

            break;
        }

        return null;
    }

    private static CancellationTokenSource GetLinkedToken(TimeSpan timeout, CancellationToken stoppingToken)
    {
        return CancellationTokenSource.CreateLinkedTokenSource(stoppingToken,
            new CancellationTokenSource(timeout).Token);
    }
}