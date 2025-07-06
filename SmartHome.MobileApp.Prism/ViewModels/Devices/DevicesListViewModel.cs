using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.MobileApp.Prism.ViewModels.Devices
{
    class DevicesListViewModel: BindableBase
    {
        private bool _isAnDeviceAvailable;

        public bool IsAnDeviceAvailable
        {
            get { return _isAnDeviceAvailable; }
            set { SetProperty(ref _isAnDeviceAvailable, value); }
        }

        public DevicesListViewModel()
        {
            IsAnDeviceAvailable = true;
        }
    }
}
