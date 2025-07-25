using SmartHome.Common.Services.CommunicationInterfaces;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SmartHome.MobileApp.Services.LocalServerServices.CommunicationInterfaces;

public class UdpServerDiscoveryInterface : IDiscoveryInterface, IDisposable
{
    private readonly UdpClient _udpClient;
    
    public UdpServerDiscoveryInterface(int serverUdpPort, int clientUdpPort)
    {
        _udpClient = new UdpClient(clientUdpPort)
        {
            EnableBroadcast = true
        };
    }

    public void Dispose()
    {
    }

    public Task<UdpReceiveResult> ReceiveDataAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }

    public Task<int> SendRequestAsync(byte[] data, IPEndPoint endPoint, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}