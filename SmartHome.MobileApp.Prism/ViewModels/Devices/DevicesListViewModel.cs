using SmartHome.MobileApp.Prism.Models.Devices;
using SmartHome.MobileApp.Services.LocalServerServices;
using System.Collections.ObjectModel;

namespace SmartHome.MobileApp.Prism.ViewModels.Devices
{
    class DevicesListViewModel: BindableBase
    {
        public DelegateCommand CheckLocalServersCommand { get; private set; } 

        public ObservableCollection<BasicDeviceModel> Devices
        {
            get { return _devices; }
            set { SetProperty(ref _devices, value); }
        }

        public bool IsAnyDeviceAvailable => Devices.Count > 0;

        private ObservableCollection<BasicDeviceModel> _devices = [];
        private readonly IServerDiscoveryService _serverDiscoveryService;
        private readonly CancellationToken _stoppingToken;

        public DevicesListViewModel(IServerDiscoveryService serverDiscoveryService)
        {
            _serverDiscoveryService = serverDiscoveryService;
            this._stoppingToken = CancellationToken.None;
            CheckLocalServersCommand = new(async () => await OnCheckLocalServersCommand());
        }

        public async Task OnCheckLocalServersCommand()
        {
            var servers = await _serverDiscoveryService.GetFirstAvailableServerAsync(_stoppingToken, TimeSpan.FromSeconds(5));

            ;
        }
    }
}
