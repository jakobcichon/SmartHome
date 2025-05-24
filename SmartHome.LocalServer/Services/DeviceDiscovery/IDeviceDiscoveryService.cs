using System.Threading.Tasks;

namespace SmartHome.LocalServer.Services.DeviceDiscovery;

public interface IDeviceDiscoveryService
{
    Task<byte[]> ReceiveDataAsync(CancellationToken stoppingToken);
    Task<int> SendDataAsync(byte[] data, CancellationToken stoppingToken);
}