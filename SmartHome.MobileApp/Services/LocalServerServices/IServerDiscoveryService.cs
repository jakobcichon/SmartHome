namespace SmartHomeClientApp.Services.LocalServerServices
{
    internal interface IServerDiscoveryService
    {
        Task<LocalServerModel> GetFirstServerAsync();
        Task<IEnumerable<LocalServerModel>> GetServersAsync();
    }
}
