using System.ComponentModel.DataAnnotations;

namespace SmartHome.LocalServer.Models.Settings
{
    public record SmartHomeSettingsModel
    {
        [Required]
        public Guid ServerGuid { get; init; }
        [Required]
        public int UdpServerPort { get; init; }
        [Required]
        public int UdpClientPort { get; init; }
        [Required]
        public string LocalDeviceCall { get; init; } = string.Empty;
        [Required]
        public string ServerCallResponse { get; init; } = string.Empty;
    }
}
