using Core.Interfaces.DataServices;
using Prism.Commands;
using Prism.Regions;
using System;
using Configuration;
using Core.LoggerExtensions;
using Core.StateMachine;
using Serilog;

namespace TChat.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly IRegionManager          _regionManager;
    private          DelegateCommand         _loginCommand;
    private readonly IUserDataService        _userDataService;
    private readonly IDataService            _dataService;
    private readonly ILogger _logger;

    public LoginViewModel(IRegionManager regionManager, IUserDataService userDataService, IDataService dataService, ILogger logger, AppConfig appConfig)
    {
        _regionManager = regionManager;

        _loginCommand         = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
        _userDataService      = userDataService;
        _dataService          = dataService;
        _logger               = logger;

        _logger.AddContext().Information("Entered LoginViewModel");
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