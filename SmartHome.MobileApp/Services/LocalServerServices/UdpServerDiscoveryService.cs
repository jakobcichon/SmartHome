using System.Net.Sockets;
using System.Text;

namespace SmartHomeClientApp.Services.LocalServerServices;

public class UdpServerDiscoveryService: IServerDiscoveryService
{
    private readonly UdpClient _udpServer;
    private readonly UdpClient _udpClient;
    
    public UdpServerDiscoveryService(int serverUdpPort, int clientUdpPort)
    {
        _udpServer = new UdpClient(serverUdpPort);
        _udpServer.EnableBroadcast = true;
        
        _udpClient = new UdpClient(clientUdpPort);
        _udpClient.EnableBroadcast = true;
    }
    
    public async Task<int> SendRequestAsync(byte[] data, CancellationToken stoppingToken)
    {
        return await _udpServer.SendAsync(data, stoppingToken);
    }

    public async Task<byte[]> ReceiveDataAsync(CancellationToken stoppingToken)
    {
        var response = await _udpClient.ReceiveAsync(stoppingToken);
        Console.WriteLine($"Received UDP message '{Encoding.UTF8.GetString(response.Buffer)}' " +
                          $"from '{response.RemoteEndPoint.Address}'");
        return response.Buffer;
    }
}