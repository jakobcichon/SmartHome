namespace SmartHome.Common.Services.CommunicationInterfaces;

public interface IDiscoveryInterface
{
    Task<int> SendRequestAsync(byte[] data, CancellationToken stoppingToken);
    Task<byte[]>  ReceiveDataAsync(CancellationToken stoppingToken);
}