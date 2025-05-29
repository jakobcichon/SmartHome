using Microsoft.Extensions.Options;
using SmartHome.Common.Models.Settings;

namespace SmartHome.MobileApp.Tests.LocalServerServices;

using Moq;

public class ServerDiscoveryServiceTests
{
    [Fact]
    public void GetFirstAvailableServerAsync_SendsRequestToAllServers_ReturnsFirstAvailableServer()
    {
        //Arrange
        var dummyClientRequestCall = "dummyClientRequestCall";
        var dummyServerResponseCall = "dummySServerResponseCall";
        var _optionsMock = new Mock<IOptions<SmartHomeCommonSettingsModel>>();
        _optionsMock.Setup(x => x.Value).Returns(new() {});

        //Act

        //Assert
    }
}