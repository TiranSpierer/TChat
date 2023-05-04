using Core.DataModels;
using Core.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;
using Core.StateMachine;

namespace TChat.ViewModels;

public class ChatViewModel : BaseViewModel
{
    private IRegionManager _regionManager;
    private IChatService _chatService;
    private string _contactName;
    private string _messageToSend;
    private Chat _chat = null!;

    public ChatViewModel(IRegionManager regionManager, IChatService chatService)
    {
        _regionManager = regionManager;
        _chatService = chatService;

        _messageToSend = string.Empty;

        SendMessageCommand = new DelegateCommand(ExecuteSendMessage);
        NavigateToChatsListCommand = new DelegateCommand(ExecuteNavigateToChatsList);
    }

    public string ContactName { get => _contactName; set => SetProperty(ref _contactName, value); }
    public string MessageToSend { get => _messageToSend; set => SetProperty(ref _messageToSend, value); }
    public DelegateCommand SendMessageCommand { get; }
    public DelegateCommand NavigateToChatsListCommand { get; }
    
    public ObservableCollection<Message> MessagesCollection { get; set; } = new();
    public User CurrentUser { get; private set; }

    private void ExecuteSendMessage()
    {
        if(string.IsNullOrEmpty(_messageToSend) == false)
        {
            var message = new Message() { 
                Text = MessageToSend, 
                FromNumber = CurrentUser.PhoneNumber, 
                ToNumber = _chat.Contact.Phone };

            _chat.Messages.Add(message);
            MessagesCollection.Add(message);
            MessageToSend = string.Empty;
        }
    }

    private void ExecuteNavigateToChatsList()
    {
        _regionManager.RequestNavigate("MainContent", new Uri("ChatsListView", UriKind.Relative));
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
        _chat = navigationContext.Parameters.GetValue<Chat>("Chat");
        if(_chat != null)
        {
            ContactName = _chat.Contact.Name ?? "Error retrieving name";

            _chat?.Messages?.ToList().ForEach(MessagesCollection.Add);
        }

        if (_regionManager.Regions["MainContent"].Context is TChatContext tChatContext)
        {
            CurrentUser = tChatContext.CurrentUser;
        }
    }

    #endregion
}