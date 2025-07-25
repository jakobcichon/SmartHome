using SmartHome.Common.Services.CommunicationInterfaces;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SmartHome.MobileApp.Services.LocalServerServices.CommunicationInterfaces;

public class UdpServerDiscoveryInterface : IDiscoveryInterface, IDisposable
{
    private readonly UdpClient _udpClient;
    private readonly int _serverUdpPort;
    
    public UdpServerDiscoveryInterface(int serverUdpPort, int clientUdpPort)
    {
        _serverUdpPort = serverUdpPort;
        _udpClient = new UdpClient(clientUdpPort)
        {
            EnableBroadcast = true
        };
    }
    
    public async Task<int> SendRequestAsync(byte[] data, IPEndPoint endPoint, CancellationToken stoppingToken)
    {
        return await _udpClient.SendAsync(data, 
            new IPEndPoint(IPAddress.Broadcast, _serverUdpPort), 
            stoppingToken);
    }

    public async Task<UdpReceiveResult> ReceiveDataAsync(CancellationToken stoppingToken)
    {
        var response = await _udpClient.ReceiveAsync(stoppingToken);
        Console.WriteLine($"Received UDP message '{Encoding.UTF8.GetString(response.Buffer)}' " +
                          $"from '{response.RemoteEndPoint.Address}'");
        return response;
    }

    public void Dispose()
    {
        _udpClient.Dispose();
    }
}