using SmartHomeClientApp.Services.LocalServerServices.Models;

namespace SmartHomeClientApp.Services.LocalServerServices
{
    internal interface IServerDiscoveryService
    {
        Task<LocalServerModel?> GetFirstAvailableServerAsync(CancellationToken stoppingToken, TimeSpan timeout);
    }
}
