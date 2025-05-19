using System.Threading.Tasks;

namespace SmartHome.LocalServer.Services;

public interface IDeviceDiscoveryPhysicalInterface
{
    public Task<byte[]> ReceiveDataAsync(CancellationToken stoppingToken);
    public Task<int> SendDataAsync(byte[] data, CancellationToken stoppingToken);
}