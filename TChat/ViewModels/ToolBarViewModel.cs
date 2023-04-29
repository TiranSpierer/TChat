

using Prism.Regions;

namespace TChat.ViewModels;

public class ToolBarViewModel : BaseViewModel
{
    private IRegionManager _regionManager;
    private string _currentView = "Login";

    public ToolBarViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        _regionManager.Regions["MainContent"].NavigationService.Navigated += NavigationService_Navigated;

    }

    private void NavigationService_Navigated(object? sender, RegionNavigationEventArgs e)
    {
        var view = e.Uri.ToString();
        CurrentView = view.Replace("View", string.Empty);
    }

    public string CurrentView { get => _currentView; set => SetProperty(ref _currentView, value); }


    #region Implementations of BaseViewModel

    public override bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public override void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
    }

    #endregion
}