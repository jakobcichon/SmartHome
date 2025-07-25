using SmartHome.Common.Services.CommunicationInterfaces;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SmartHome.LocalServer.Services.DeviceDiscovery.CommunicationInterfaces;

public class UdpDeviceDiscoveryInterface : IDiscoveryInterface, IDisposable
{
    private readonly UdpClient _udpClient;
    
    public UdpDeviceDiscoveryInterface(int serverUdpPort)
    {
        _udpClient = new UdpClient(serverUdpPort)
        {
            EnableBroadcast = true
        };
    }
    
    public async Task<UdpReceiveResult> ReceiveDataAsync(CancellationToken stoppingToken)
    {
        var response = await _udpClient.ReceiveAsync(stoppingToken);
        Console.WriteLine($"Received UDP message '{Encoding.UTF8.GetString(response.Buffer)}' " +
                          $"from '{response.RemoteEndPoint.Address}'");
        return response;
    }

    public async Task<int> SendRequestAsync(byte[] data, IPEndPoint endPoint, CancellationToken stoppingToken)
    {
        return await _udpClient.SendAsync(data, endPoint, stoppingToken);
    }

    public void Dispose()
    {
        _udpClient.Dispose();
    }
}