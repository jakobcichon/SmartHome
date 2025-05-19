using System.Threading.Tasks;

namespace SmartHome.LocalServer.Services.DeviceDiscovery;

public interface IDeviceDiscoveryCommunicationInterface
{
    public Task<byte[]> ReceiveDataAsync(CancellationToken stoppingToken);
    public Task<int> SendDataAsync(byte[] data, CancellationToken stoppingToken);
}