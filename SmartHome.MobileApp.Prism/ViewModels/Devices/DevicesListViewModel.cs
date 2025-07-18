using SmartHome.MobileApp.Prism.Models.Devices;
using System.Collections.ObjectModel;

namespace SmartHome.MobileApp.Prism.ViewModels.Devices
{
    class DevicesListViewModel: BindableBase
    {
        public bool IsAnyDeviceAvailable => Devices.Count > 0;

        private ObservableCollection<BasicDeviceModel> _devices = [];

        public ObservableCollection<BasicDeviceModel> Devices
        {
            get { return _devices; }
            set { SetProperty(ref _devices, value); }
        }
    }
}
