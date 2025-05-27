namespace SmartHomeClientApp.Services.LocalServerServices;

public interface IServerDiscoveryInterface
{
    Task<int> SendRequestAsync(byte[] data, CancellationToken stoppingToken);
    Task<byte[]>  ReceiveDataAsync(CancellationToken stoppingToken);
}