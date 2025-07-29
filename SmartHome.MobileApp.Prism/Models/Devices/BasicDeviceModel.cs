using Newtonsoft.Json.Converters;
using SmartHome.MobileApp.Prism.Types.Device;
using System.Text.Json.Serialization;

namespace SmartHome.MobileApp.Prism.Models.Devices
{
    record BasicDeviceModel
    {
        public string Name { get; set; } = "DefaultDevice";
        public string Type { get; set; } = "DefualtType";
       
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceStatusEnum Status { get; set; } = DeviceStatusEnum.Unknown;

        public Guid Guid { get; set; } = Guid.Empty;
    }
}
