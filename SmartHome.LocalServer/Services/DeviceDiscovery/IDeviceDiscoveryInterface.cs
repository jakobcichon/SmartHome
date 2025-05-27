namespace SmartHome.LocalServer.Services.DeviceDiscovery;

public interface IDeviceDiscoveryInterface
{
    Task<byte[]> ReceiveDataAsync(CancellationToken stoppingToken);
    Task<int> SendDataAsync(byte[] data, CancellationToken stoppingToken);
}