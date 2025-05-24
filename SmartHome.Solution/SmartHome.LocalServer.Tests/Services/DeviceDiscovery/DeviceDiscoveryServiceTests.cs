using Microsoft.Extensions.Options;
using Moq;
using SmartHome.Common.Extensions.String;
using SmartHome.LocalServer.Models.Settings;
using SmartHome.LocalServer.Services.DeviceDiscovery;

namespace SmartHome.LocalServer.Tests.Services.DeviceDiscovery;

public class DeviceDiscoveryServiceTests
{
    IOptions<SmartHomeSettingsModel> _options;
    public DeviceDiscoveryServiceTests()
    {
        _options = Options.Create(
            new SmartHomeSettingsModel()
            {
                LocalDeviceCall = "dummyLocalCall",
                ServerCallResponse = "dummyServerResponse"
            });
    }

    [Fact]
    public void ExecuteAsync_ValidDeviceRequest_CallsTheRespondMethod()
    {
        //Arrange
        var mock = new Mock<IDeviceDiscoveryService>();
        var stoppingtokenSource = new CancellationTokenSource();

        mock.Setup(e => e.ReceiveDataAsync(It.IsAny<CancellationToken>()))
            .Callback((CancellationToken token) =>
            {
                stoppingtokenSource.Cancel();
            }).ReturnsAsync(_options.Value.LocalDeviceCall.ToUtf8());

        var act = new DeviceDiscoveryService(mock.Object, _options);

        //Act
        act.StartAsync(stoppingtokenSource.Token);

        //Assert
        mock.Verify(e => e.ReceiveDataAsync(
            It.IsAny<CancellationToken>()), Times.Once());
        mock.Verify(e => e.SendDataAsync(
            _options.Value.ServerCallResponse.ToUtf8(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public void ExecuteAsync_InvalidDeviceRequest_DontCallTheRespondMethod()
    {
        //Arrange
        var mock = new Mock<IDeviceDiscoveryService>();
        var stoppingtokenSource = new CancellationTokenSource();

        mock.Setup(e => e.ReceiveDataAsync(It.IsAny<CancellationToken>()))
            .Callback((CancellationToken token) =>
            {
                stoppingtokenSource.Cancel();
            }).ReturnsAsync("incorrectDeviceCall".ToUtf8());

        var act = new DeviceDiscoveryService(mock.Object, _options);

        //Act
        act.StartAsync(stoppingtokenSource.Token);

        //Assert
        mock.Verify(e => e.ReceiveDataAsync(
            It.IsAny<CancellationToken>()), Times.Once());
        mock.Verify(e => e.SendDataAsync(It.IsAny<byte[]>(), It.IsAny<CancellationToken>()), Times.Never());

    }
}