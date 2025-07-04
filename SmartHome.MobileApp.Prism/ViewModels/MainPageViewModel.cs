
namespace SmartHome.MobileApp.Prism.ViewModels
{
    internal class MainPageViewModel : BindableBase, INavigationAware
    {
        public string MainPageRegionName { get; } = "MainPageContent";
        public DelegateCommand<INavigationParameters> NavigateToHomeCommand { get; private set; }
        public DelegateCommand<INavigationParameters> NavigateToMenuCommand { get; private set; }

        private readonly IRegionManager _regionManager;

        public MainPageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateToHomeCommand = new(OnNavigatedTo);
            NavigateToMenuCommand = new(OnNavigateToMenu);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
          
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _regionManager.RequestNavigate(MainPageRegionName, "HomeView");
        }

        public void OnNavigateToMenu(INavigationParameters parameters)
        {
            _regionManager.RequestNavigate(MainPageRegionName, "MenuView");
        }


    }
}
