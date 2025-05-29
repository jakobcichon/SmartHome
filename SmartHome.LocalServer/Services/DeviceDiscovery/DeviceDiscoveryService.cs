using System.Text;
using Microsoft.Extensions.Options;
using SmartHome.Common.Extensions.String;
using SmartHome.Common.Models.Settings;
using SmartHome.LocalServer.Services.DeviceDiscovery.CommunicationInterfaces;


namespace SmartHome.LocalServer.Services.DeviceDiscovery
{
    public class DeviceDiscoveryService(IDeviceDiscoveryInterface commInterface, 
        IOptions<SmartHomeCommonSettingsModel> options) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = Encoding.UTF8.GetString(await commInterface.ReceiveDataAsync(stoppingToken));
                await SendResponseIfNeeded(result, stoppingToken);
            }
        }

        private async Task SendResponseIfNeeded(string result, CancellationToken stoppingToken)
        {
            if (result == options.Value.LocalDeviceCall)
            {
                await commInterface.SendDataAsync((options.Value.ServerCallResponse + options.Value.ServerGuid).ToUtf8(), 
                    stoppingToken);
            }
        }
    }
}