using SmartHome.MobileApp.Models;
using SmartHome.MobileApp.Prism.Models.Devices;
using SmartHome.MobileApp.Prism.Types.Device;
using SmartHome.MobileApp.Repositories.LocalServer;
using SmartHome.MobileApp.Services.LocalServerServices;
using System.Collections.ObjectModel;

namespace SmartHome.MobileApp.Prism.ViewModels.Devices
{
    class DevicesListViewModel : BindableBase
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
        private readonly ILocalServerRepository _serverRepository;
        private readonly CancellationToken _stoppingToken;

        public DevicesListViewModel(IServerDiscoveryService serverDiscoveryService, ILocalServerRepository serverRepository)
        {
            _serverDiscoveryService = serverDiscoveryService;
            _serverRepository = serverRepository;
            _stoppingToken = CancellationToken.None;
            CheckLocalServersCommand = new(async () => await OnCheckLocalServersCommand());
        }

        public async Task OnCheckLocalServersCommand()
        {
            var server = await _serverDiscoveryService.GetFirstAvailableServerAsync(_stoppingToken, TimeSpan.FromSeconds(5));
            if (server is null) return;

            foreach (var device in await _serverRepository.GetAllAsync(_stoppingToken))
            {
                if (device is null) continue;

                _devices.Add(new()
                {
                    Guid = device.Guid,
                    Name = device.Name ?? string.Empty,
                    Status = DeviceStatusEnum.Offline
                });
            }

            if (await _serverRepository.GetAsync(server.Guid, _stoppingToken) is LocalServerModel foundServer)
            {

                return;
            }
        }
    }
}
