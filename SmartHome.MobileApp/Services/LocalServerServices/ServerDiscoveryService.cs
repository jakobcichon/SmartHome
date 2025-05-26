namespace SmartHomeClientApp.Services.LocalServerServices;

public class ServerDiscoveryService : IServerDiscoveryService
{
    public Task<int> SendRequestAsync(byte[] data, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> ReceiveDataAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}