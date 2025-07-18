using SmartHome.MobileApp.Models;

namespace SmartHome.MobileApp.Repositories.LocalServer;

public interface ILocalServerRepository
{
    Task<bool> CreateAsync(LocalServerModel model, CancellationToken stoppingToken);
    Task<bool> UpdateAsync(LocalServerModel model, CancellationToken stoppingToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken stoppingToken);
    Task<LocalServerModel?> GetAsync(Guid id, CancellationToken stoppingToken);
    Task<IEnumerable<LocalServerModel>> GetAllAsync(CancellationToken stoppingToken);
}