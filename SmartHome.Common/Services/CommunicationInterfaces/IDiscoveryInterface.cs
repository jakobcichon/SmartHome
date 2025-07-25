using System.Net;
using System.Net.Sockets;

namespace SmartHome.Common.Services.CommunicationInterfaces;

public interface IDiscoveryInterface
{
    Task<int> SendRequestAsync(byte[] data, IPEndPoint endPoint,  CancellationToken stoppingToken);
    Task<UdpReceiveResult> ReceiveDataAsync(CancellationToken stoppingToken);
}