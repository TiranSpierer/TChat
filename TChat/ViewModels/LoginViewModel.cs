

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
    private IRegionManager _regionManager;
    private DelegateCommand _loginCommand;
    private IUserDataService _userDataService;
    private IMongoDbDataService _mongoDbDataService;

    public LoginViewModel(IRegionManager regionManager, IUserDataService userDataService, IMongoDbDataService mongoDbDataService)
    {
        _regionManager = regionManager;

        _loginCommand       = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
        _userDataService    = userDataService;
        _mongoDbDataService = mongoDbDataService;

        _ = DoSomething();
    }

    private async Task DoSomething()
    {
        await _mongoDbDataService.AddUserAsync(new User()
        {
            Username = "Tyrone"
        });

        var x = await _mongoDbDataService.GetAllUsersAsync();

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