using System.ComponentModel.DataAnnotations;

namespace SmartHome.Common.Models.Settings
{
    public record SmartHomeCommonSettingsModel
    {
        [Required]
        public Guid ServerGuid { get; init; }
        [Required]
        public int ClientUdpPort { get; init; }
        [Required]
        public int ServerUdpPort { get; init; }
        [Required]
        public string LocalDeviceCall { get; init; } = string.Empty;
        [Required]
        public string ServerCallResponse { get; init; } = string.Empty;
    }
}
