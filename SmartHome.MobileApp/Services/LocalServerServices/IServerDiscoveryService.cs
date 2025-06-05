using SmartHome.MobileApp.Models;

namespace SmartHome.MobileApp.Services.LocalServerServices
{
    internal interface IServerDiscoveryService
    {
        Task<LocalServerModel?> GetFirstAvailableServerAsync(CancellationToken stoppingToken, TimeSpan timeout);
    }
}
