using SmartHomeClientApp.Services.LocalServerServices.Models;

namespace SmartHomeClientApp.Services.LocalServerServices
{
    internal interface IServerDiscoveryService
    {
        Task<LocalServerModel> GetFirstAvailableServerAsync(TimeSpan timeout, CancellationToken stoppingToken);
    }
}
