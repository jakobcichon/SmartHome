namespace SmartHomeClientApp.Services.LocalServerServices.CommunicationInterfaces;

public interface IServerDiscoveryInterface
{
    Task<int> SendRequestAsync(byte[] data, CancellationToken stoppingToken);
    Task<byte[]>  ReceiveDataAsync(CancellationToken stoppingToken, TimeSpan timeout);
}