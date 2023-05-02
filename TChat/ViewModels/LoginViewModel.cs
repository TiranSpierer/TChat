

using Core.DataModels;
using Core.Interfaces.DataServices;
using Core.State;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using ZstdSharp.Unsafe;

namespace TChat.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly IRegionManager      _regionManager;
    private          DelegateCommand     _loginCommand;
    private readonly IUserDataService    _userDataService;
    private readonly IDataService _dataService;

    public LoginViewModel(IRegionManager regionManager, IUserDataService userDataService, IDataService dataService)
    {
        _regionManager = regionManager;

        _loginCommand       = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
        _userDataService    = userDataService;
        _dataService = dataService;

        _ = DoSomething();
    }

    private async Task DoSomething()
    {
        await _dataService.AddUserAsync(new User()
        {
            Username = "Tiran"
        });

        var x = await _dataService.GetAllUsersAsync();

        foreach (var y in x)
        {
            Debug.WriteLine(y.Username);
        }
    }


    public DelegateCommand LoginCommand { get => _loginCommand; set => _loginCommand = value; }

    private void ExecuteLogin()
    {
        _regionManager.Regions["MainContent"].Context = new TChatContext { CurrentUser = _userDataService.GetUser("") };

        _regionManager.RequestNavigate("MainContent", new Uri("ChatsListView", UriKind.Relative));
    }

    private bool CanExecuteLogin() => true;

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