using System.Text;
using Microsoft.Extensions.Options;
using Moq;
using SmartHome.Common.Extensions.String;
using SmartHome.LocalServer.Models.Settings;
using SmartHome.LocalServer.Services.DeviceDiscovery;

namespace SmartHome.LocalServer.Tests.Services.DeviceDiscovery;

public class DeviceDiscoveryServiceTests
{
    [Fact]
    public void ExecuteAsync_DummyDeviceRequest_CallsTheRespondMethod()
    {
        //Arrange
        var options = Options.Create(
            new SmartHomeSettingsModel()
            {
                LocalDeviceCall = "dummyLocalCall",
                ServerCallResponse = "serverResponse"
            });
        var mock = new Mock<IDeviceDiscoveryCommunicationInterface>();
        mock.Setup(e => e.ReceiveDataAsync(CancellationToken.None)).
            Returns(Task.FromResult(options.Value.LocalDeviceCall.ToUtf8()));
        
        var dut = new DeviceDiscoveryService(mock.Object, options);
       
        //Act
        dut.StartAsync(CancellationToken.None);

        //Assert
        mock.Verify(e => e.ReceiveDataAsync(
            It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        mock.Verify(e => e.SendDataAsync(
            options.Value.ServerCallResponse.ToUtf8(), It.IsAny<CancellationToken>()));

    }
}