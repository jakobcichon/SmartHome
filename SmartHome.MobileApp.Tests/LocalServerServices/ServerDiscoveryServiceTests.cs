using Microsoft.Extensions.Options;
using Moq;
using SmartHome.Common.Extensions.String;
using SmartHome.Common.Models.Settings;
using SmartHome.Common.Services.CommunicationInterfaces;
using SmartHome.MobileApp.Services.LocalServerServices;

namespace SmartHome.MobileApp.Tests.LocalServerServices;


public class ServerDiscoveryServiceTests
{
    IOptions<SmartHomeCommonSettingsModel> _options;
    public ServerDiscoveryServiceTests()
    {
        _options = Options.Create(
            new SmartHomeCommonSettingsModel()
            {
                LocalDeviceCall = "dummyLocalCall",
                ServerCallResponse = "dummyServerResponse",
                ServerGuid = Guid.NewGuid(),
            });
    }

    [Fact]
    public async Task GetFirstAvailableServerAsync_SendsRequestToAllServers_ReturnsFirstAvailableServer()
    {
        //Arrange
        var stoppingTokenSource = new CancellationTokenSource();
        var _discoveryInterfaceMock = new Mock<IDiscoveryInterface>();
        var serverResponse = GetServerResponseWithGuid().ToUtf8();

        _discoveryInterfaceMock.Setup(e => e.SendRequestAsync(It.IsAny<byte[]>(), It.IsAny<CancellationToken>())).ReturnsAsync(0);
        _discoveryInterfaceMock.Setup(e => e.ReceiveDataAsync(It.IsAny<CancellationToken>()))
        .Callback((CancellationToken token) =>
        {
            stoppingTokenSource.Cancel();
        }).ReturnsAsync(serverResponse);

        var _act = new ServerDiscoveryService(_discoveryInterfaceMock.Object, _options);

        //Act
        var result = await _act.GetFirstAvailableServerAsync(stoppingTokenSource.Token, TimeSpan.FromDays(1));

        //Assert
        _discoveryInterfaceMock.Verify(e => e.SendRequestAsync(_options.Value.LocalDeviceCall.ToUtf8(),
            It.IsAny<CancellationToken>()), Times.Once);
        _discoveryInterfaceMock.Verify(e => e.ReceiveDataAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(result);
        Assert.True(result.Guid.Equals(_options.Value.ServerGuid));

    }

    private string GetServerResponseWithGuid()
    {
        return _options.Value.ServerCallResponse + _options.Value.ServerGuid;
    }
}