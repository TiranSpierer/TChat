using Prism.Regions;
using TChat.Views;

namespace TChat.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private readonly IRegionManager _regionManager;

    public MainWindowViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }


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