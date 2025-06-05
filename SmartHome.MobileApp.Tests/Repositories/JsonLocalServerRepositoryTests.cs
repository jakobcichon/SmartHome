using SmartHome.MobileApp.Models;
using SmartHome.MobileApp.Repositories.LocalServer;

namespace SmartHome.MobileApp.Tests.Repositories;

public class JsonLocalServerRepositoryTests: IDisposable
{
    private readonly string _filePath =
        Path.Join(Path.GetTempPath(), "SmartHome.MobileApp", 
            nameof(JsonLocalServerRepositoryTests), "LocalServerData.json");

    [Fact]
    public async Task CreateAsync_ModelCreatedSuccessfully()
    {
        // Arrange
        var dummyModel = new LocalServerModel { Guid = Guid.NewGuid(), Name = "Server1TestName" };
        var act = new JsonLocalServerRepository(_filePath);

        // Act
        var result = await act.CreateAsync(dummyModel, CancellationToken.None);

        // Assert
        Assert.True(result);
    }
    
    public void Dispose()
    {
        if (File.Exists(_filePath)) File.Delete(_filePath);
    }
}