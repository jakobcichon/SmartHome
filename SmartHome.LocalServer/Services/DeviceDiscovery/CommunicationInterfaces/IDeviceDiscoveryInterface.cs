namespace SmartHome.LocalServer.Services.DeviceDiscovery.CommunicationInterfaces;

public interface IDeviceDiscoveryInterface
{
    Task<byte[]> ReceiveDataAsync(CancellationToken stoppingToken);
    Task<int> SendDataAsync(byte[] data, CancellationToken stoppingToken);
}