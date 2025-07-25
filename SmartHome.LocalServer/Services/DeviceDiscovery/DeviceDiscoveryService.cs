using Microsoft.Extensions.Options;
using SmartHome.Common.Extensions.String;
using SmartHome.Common.Models.Settings;
using SmartHome.Common.Services.CommunicationInterfaces;
using System.Net;
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
                var request = await commInterface.ReceiveDataAsync(stoppingToken);
                var deviceCall = Encoding.UTF8.GetString(request.Buffer);
                await SendResponseIfRequestCallMatch(deviceCall, request.RemoteEndPoint, stoppingToken);
            }
        }

        private async Task SendResponseIfRequestCallMatch(string deviceCall, IPEndPoint endPoint, CancellationToken stoppingToken)
        {
            if (deviceCall == options.Value.LocalDeviceCall)
            {
                await commInterface.SendRequestAsync((options.Value.ServerCallResponse + options.Value.ServerGuid).ToUtf8(),
                    endPoint,
                    stoppingToken);
            }
        }
    }
}