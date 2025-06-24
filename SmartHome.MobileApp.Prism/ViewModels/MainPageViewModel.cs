
namespace SmartHome.MobileApp.Prism.ViewModels
{
    internal class MainPageViewModel : BindableBase, INavigationAware
    {
        public string MainPageRegionName { get; } = "MainPageContent";

        private readonly IRegionManager _regionManager;

        public MainPageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            ;
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _regionManager.RequestNavigate(MainPageRegionName, "DevicesView");
        }
    }
}
