namespace SmartHome.MobileApp.Prism.Models.Devices
{
    record BasicDeviceModel
    {
        public string Name { get; set; } = "DefaultDevice";
        public string Type { get; set; } = "DefualtType";
        public string Status { get; set; } = "DefualtStatus";
    }
}
