using System.Text.Json;
using SmartHome.MobileApp.Models;

namespace SmartHome.MobileApp.Repositories.LocalServer;

public class JsonLocalServerRepository(string filePath) : ILocalServerRepository
{
    private readonly string _filePath = filePath;

    public async Task<bool> CreateAsync(LocalServerModel model, CancellationToken stoppingToken)
    {
        var servers = await LoadDataFromFile(stoppingToken);
        if (!servers.TryAdd(model.Guid, model)) return false;
        
        return await WriteDataFromFile(servers, stoppingToken);
    }

    public async Task<bool> UpdateAsync(LocalServerModel model, CancellationToken stoppingToken)
    {
        var servers = await LoadDataFromFile(stoppingToken);
        if (!servers.ContainsKey(model.Guid)) return false;
        
        servers[model.Guid] = model;
        
        return await WriteDataFromFile(servers, stoppingToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken stoppingToken)
    {
        var servers = await LoadDataFromFile(stoppingToken);
        if (!servers.Remove(id)) return false;
        
        return await WriteDataFromFile(servers, stoppingToken);
    }

    public async Task<LocalServerModel?> GetAsync(Guid id, CancellationToken stoppingToken)
    {
        var servers = await LoadDataFromFile(stoppingToken);
        return servers.GetValueOrDefault(id);
    }

    public async Task<IEnumerable<LocalServerModel>> GetAllAsync(CancellationToken stoppingToken)
    {
        var servers = await LoadDataFromFile(stoppingToken);
        return servers.Values;
    }

    private async Task<Dictionary<Guid, LocalServerModel>> LoadDataFromFile(CancellationToken stoppingToken)
    {
        if (Directory.GetParent(_filePath) is { } directoryInfo) Directory.CreateDirectory(directoryInfo.FullName);
        
        await using var fileStream = new FileStream(_filePath, FileMode.OpenOrCreate);
        if (fileStream.Length == 0) return new Dictionary<Guid, LocalServerModel>();
        
        var servers = await JsonSerializer.DeserializeAsync<Dictionary<Guid, LocalServerModel>>(fileStream, 
            JsonSerializerOptions.Default, stoppingToken) ?? new Dictionary<Guid, LocalServerModel>();
        
        return servers; 
    }
    
    private async Task<bool> WriteDataFromFile(Dictionary<Guid, LocalServerModel> data, CancellationToken stoppingToken)
    {
        await using var streamWriter = new StreamWriter(_filePath);

        try
        {
            await JsonSerializer.SerializeAsync(streamWriter.BaseStream,
                data, JsonSerializerOptions.Default, stoppingToken);
        }
        catch
        {
            return false;
        }
        
        return true;
    }
}