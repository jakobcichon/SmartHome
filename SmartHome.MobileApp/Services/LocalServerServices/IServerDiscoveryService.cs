namespace SmartHomeClientApp.Services.LocalServerServices;

public interface IServerDiscoveryService
{
    Task<int> SendRequestAsync(byte[] data, CancellationToken stoppingToken);
    Task<byte[]>  ReceiveDataAsync(CancellationToken stoppingToken);
}