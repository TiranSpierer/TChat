using Core.DataModels;
using Core.State;
using Prism.Commands;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;

namespace TChat.ViewModels;

public class ChatsListViewModel : BaseViewModel
{
    private readonly IRegionManager _regionManager;

    public ChatsListViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        NavigateToChatCommand = new DelegateCommand<Chat>(ExecuteNavigateToChat);
    }

    public User CurrentUser { get; set; }

    public ObservableCollection<Chat> ChatsCollection { get; set; } = new();

    public DelegateCommand<Chat> NavigateToChatCommand { get; }

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
        ChatsCollection.Clear();

        if (_regionManager.Regions["MainContent"].Context is TChatContext tChatContext)
        {
            CurrentUser = tChatContext.CurrentUser;
            CurrentUser?.Chats?.ToList().ForEach(ChatsCollection.Add);
        }
    }

    #endregion

    private void ExecuteNavigateToChat(Chat chat)
    {
        var parameters = new NavigationParameters
        {
            { "Chat", chat }
        };

        _regionManager.RequestNavigate("MainContent", "ChatView", parameters);
    }
}