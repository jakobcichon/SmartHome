namespace SmartHome.LocalServer.Models.Settings
{
    public record SmartHomeSettingsModel
    {
        public int? LocalDeviceUdpPort { get; init; }
        public string LocalDeviceCall { get; init; } = string.Empty;
        public string ServerCallResponse { get; init; } = string.Empty;
        
        public bool IsLocalDeviceUdpPortAvailable() => LocalDeviceUdpPort != null;
    }
}
