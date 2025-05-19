using System.Net.Sockets;
using System.Text;

namespace SmartHome.LocalServer.Services.DeviceDiscovery;

public class UdpDeviceDiscoveryAbstraction : IDeviceDiscoveryPhysicalInterface, IDisposable
{
    private readonly UdpClient _udpServer;
    private readonly UdpClient _udpClient;
    
    public UdpDeviceDiscoveryAbstraction(int serverUdpPort, int clientUdpPort)
    {
        _udpServer = new UdpClient(serverUdpPort);
        _udpServer.EnableBroadcast = true;
        
        _udpClient = new UdpClient(clientUdpPort);
        _udpClient.EnableBroadcast = true;
    }
    
    public async Task<byte[]> ReceiveDataAsync(CancellationToken stoppingToken)
    {
        var response = await _udpServer.ReceiveAsync(stoppingToken);
        Console.WriteLine($"Received UDP message '{Encoding.UTF8.GetString(response.Buffer)}' " +
                          $"from '{response.RemoteEndPoint.Address}'");
        return response.Buffer;
    }

    public async Task<int> SendDataAsync(byte[] data, CancellationToken stoppingToken)
    {
        return await _udpClient.SendAsync(data, stoppingToken);
    }

    public void Dispose()
    {
        _udpServer.Dispose();
        _udpClient.Dispose();
    }
}