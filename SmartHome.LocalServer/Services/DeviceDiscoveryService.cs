
using System.Net;
using SmartHome.LocalServer.Models.Settings;
using Microsoft.Extensions.Options;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

namespace SmartHome.LocalServer.Services
{
    public class DeviceDiscoveryService(IOptions<SmartHomeSettingsModel> options) : BackgroundService
    {
        private readonly SmartHomeSettingsModel _options = options.Value;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_options.IsLocalDeviceUdpPortAvailable()) return;

            var port = this._options.LocalDeviceUdpPort!.Value;

            using UdpClient udpClient = new(port);
            udpClient.EnableBroadcast = true;

            while (!stoppingToken.IsCancellationRequested)
            {
                await ListenForTheMessageAndRespond(port, udpClient, stoppingToken);
            }
        }

        private async Task ListenForTheMessageAndRespond(int port, UdpClient udpClient, CancellationToken stoppingToken)
        {
            var result = await udpClient.ReceiveAsync(stoppingToken);

            Debug.WriteLine($"ReceivedMessage UDP broadcast: {Encoding.UTF8.GetString(result.Buffer)} from: {result.RemoteEndPoint.Address}");

            if (IsHandshakeMessage(result))
            {
                await SendResponse(udpClient, port, stoppingToken);
            }
        }

        private bool IsHandshakeMessage(UdpReceiveResult result)
        {
            return Encoding.UTF8.GetString(result.Buffer) == _options.LocalDeviceCall;
        }

        private async Task SendResponse(UdpClient udpClient, int port, CancellationToken stoppingToken)
        {
            var dataBytes = Encoding.UTF8.GetBytes($"{_options.ServerCallResponse} GUID: {}");
            await udpClient.SendAsync(dataBytes, CreateBroadcastIpEndPoint(port), stoppingToken);
        }

        private static IPEndPoint CreateBroadcastIpEndPoint(int port)
        {
            return new IPEndPoint(IPAddress.Broadcast, port);
        }
    }
}
