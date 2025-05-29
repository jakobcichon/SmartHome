using Microsoft.Extensions.Options;
using SmartHome.Common.Extensions.String;
using SmartHome.Common.Models.Settings;
using SmartHome.Common.Services.CommunicationInterfaces;
using System.Text;


namespace SmartHome.LocalServer.Services.DeviceDiscovery
{
    public class DeviceDiscoveryService(IDiscoveryInterface commInterface, 
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
                await commInterface.SendRequestAsync((options.Value.ServerCallResponse + options.Value.ServerGuid).ToUtf8(), 
                    stoppingToken);
            }
        }
    }
}