
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
            using UdpClient udpClient = new(this._options.UdpServerPort);
            udpClient.EnableBroadcast = true;

            while (!stoppingToken.IsCancellationRequested)
            {
                await ListenForTheMessageAndRespond(this._options.UdpClientPort, udpClient, stoppingToken);
            }
        }

        private async Task ListenForTheMessageAndRespond(int clientPort, UdpClient udpClient, CancellationToken stoppingToken)
        {
            var result = await udpClient.ReceiveAsync(stoppingToken);

            Debug.WriteLine($"ReceivedMessage UDP broadcast: {Encoding.UTF8.GetString(result.Buffer)} from: {result.RemoteEndPoint.Address}");

            if (IsHandshakeMessage(result))
            {
                await SendResponse(udpClient, clientPort, stoppingToken);
            }
        }

        private bool IsHandshakeMessage(UdpReceiveResult result)
        {
            return Encoding.UTF8.GetString(result.Buffer) == _options.LocalDeviceCall;
        }

        private async Task SendResponse(UdpClient udpClient, int port, CancellationToken stoppingToken)
        {
            var dataBytes = Encoding.UTF8.GetBytes($"{_options.ServerCallResponse} GUID: {_options.ServerGuid}");
            await udpClient.SendAsync(dataBytes, CreateBroadcastIpEndPoint(port), stoppingToken);
        }

        private static IPEndPoint CreateBroadcastIpEndPoint(int port)
        {
            return new IPEndPoint(IPAddress.Broadcast, port);
        }
    }
}
