namespace SmartHome.LocalServer.Models.Settings
{
    public record SmartHomeSettingsModel
    {
        public int ClientUdpPort { get; init; }
        public int ServerUdpPort { get; init; }
        public string LocalDeviceCall { get; init; } = string.Empty;
        public string ServerCallResponse { get; init; } = string.Empty;
    }
}
