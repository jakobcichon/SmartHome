
using System.Net;
using SmartHome.LocalServer.Models.Settings;
using Microsoft.Extensions.Options;
using System.Net.Sockets;
using System.Text;

namespace SmartHome.LocalServer.Services
{
    public class DeviceDiscoveryService(IOptions<SmartHomeSettingsModel> options) : BackgroundService
    {
        private readonly SmartHomeSettingsModel _options = options.Value;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_options.IsLocalDeviceUdpPortAvailable())
            {
                return;
            }

            var port = this._options.LocalDeviceUdpPort!.Value;

            using UdpClient udpClient = new(port);
            udpClient.EnableBroadcast = true;

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await udpClient.ReceiveAsync(stoppingToken);
                Console.WriteLine($"ReceivedMessage UDP broadcast: {Encoding.UTF8.GetString(result.Buffer)} from: {result.RemoteEndPoint.Address}");
                
                if (Encoding.UTF8.GetString(result.Buffer) == _options.LocalDeviceCall)
                {
                    await SendResponse(udpClient, stoppingToken);
                }
            }
        }

        private async Task SendResponse(UdpClient udpClient, CancellationToken stoppingToken)
        {
            var dataBytes = Encoding.UTF8.GetBytes(_options.ServerCallResponse);
            await udpClient.SendAsync(dataBytes,
                                        new IPEndPoint(IPAddress.Broadcast,
                                        _options.LocalDeviceUdpPort.Value),
                                        stoppingToken);
        }
    }
}
