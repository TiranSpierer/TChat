using Core.DataModels;
using Core.State;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;

namespace TChat.ViewModels;

public class ContactsViewModel : BaseViewModel
{
    private IRegionManager _regionManager;

    public ContactsViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        NavigateToChatCommand = new DelegateCommand<Chat>(ExecuteNavigateToChat);
    }

    public User CurrentUser { get; set; }

    public ObservableCollection<Contact> ContactsCollection { get; set; } = new();

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
        ContactsCollection.Clear();

        if (_regionManager.Regions["MainContent"].Context is TChatContext tChatContext)
        {
            CurrentUser = tChatContext.CurrentUser;
            CurrentUser?.Contacts?.ToList().ForEach(ContactsCollection.Add);
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
