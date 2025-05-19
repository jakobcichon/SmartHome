using System.Text;
using Microsoft.Extensions.Options;
using SmartHome.LocalServer.Models.Settings;

namespace SmartHome.LocalServer.Services.DeviceDiscovery
{
    public class DeviceDiscoveryService(IDeviceDiscoveryPhysicalInterface _interface, 
        IOptions<SmartHomeSettingsModel> _options) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = Encoding.UTF8.GetString(await _interface.ReceiveDataAsync(stoppingToken));
                await SendResponseIfNeeded(result, stoppingToken);
            }
        }

        private async Task SendResponseIfNeeded(string result, CancellationToken stoppingToken)
        {
            if (result == _options.Value.LocalDeviceCall)
            {
                await _interface.SendDataAsync(Encoding.UTF8.GetBytes(_options.Value.ServerCallResponse), stoppingToken);
            }
        }
    }
}