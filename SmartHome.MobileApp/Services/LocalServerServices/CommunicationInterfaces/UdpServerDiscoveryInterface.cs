using System.Net.Sockets;
using System.Text;

namespace SmartHomeClientApp.Services.LocalServerServices.CommunicationInterfaces;

public class UdpServerDiscoveryInterface : IServerDiscoveryInterface, IDisposable
{
    private readonly UdpClient _udpServer;
    private readonly UdpClient _udpClient;
    
    public UdpServerDiscoveryInterface(int serverUdpPort, int clientUdpPort)
    {
        _udpServer = new UdpClient(serverUdpPort)
        {
            EnableBroadcast = true
        };

        _udpClient = new UdpClient(clientUdpPort)
        {
            EnableBroadcast = true
        };
    }
    
    public async Task<int> SendRequestAsync(byte[] data, CancellationToken stoppingToken)
    {
        return await _udpServer.SendAsync(data, stoppingToken);
    }

    public async Task<byte[]> ReceiveDataAsync(CancellationToken stoppingToken, TimeSpan timeout)
    {
        _udpClient.Client.ReceiveTimeout = timeout.TotalMilliseconds;
        var response = await _udpClient.ReceiveAsync(stoppingToken);
        Console.WriteLine($"Received UDP message '{Encoding.UTF8.GetString(response.Buffer)}' " +
                          $"from '{response.RemoteEndPoint.Address}'");
        return response.Buffer;
    }

    public void Dispose()
    {
        _udpClient.Dispose();
        _udpServer.Dispose();
    }
}