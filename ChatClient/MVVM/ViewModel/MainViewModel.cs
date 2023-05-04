using Prism.Commands;
using Prism.Mvvm;
using Core.Net;
using System;
using ChatClient.MVVM.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ChatClient.MVVM.ViewModel;

public class MainViewModel : BindableBase
{
    private Server _server;
    private string _username;
    private string _message;

    public MainViewModel()
    {
        _server = new Server();
        _username = _message =string.Empty;

        Users = new ObservableCollection<UserModel>();
        Messages = new ObservableCollection<string>();

        ConnectToServerCommand = new DelegateCommand(ExecuteConnectToServer, CanExecuteConnectToServer);
        SendMessageCommand = new DelegateCommand(ExecuteSendMessage, CanExecuteSendMessage);

        _server.ConnectionEvent += UserConnectionEvent;
        _server.MessageRecievedEvent += MessageRecievedEvent;
        _server.UserDisconnectEvent += UserDisconnectEvent;
    }



    public DelegateCommand ConnectToServerCommand { get; set; }
    public DelegateCommand SendMessageCommand { get; set; }
    public ObservableCollection<UserModel> Users { get; set; }
    public ObservableCollection<string> Messages { get; set; }

    public string Username 
    { 
        get => _username; 
        set 
        { 
            SetProperty(ref _username, value); 
            ConnectToServerCommand.RaiseCanExecuteChanged();
        } 
    }

    public string Message
    {
        get => _message;
        set
        {
            SetProperty(ref _message, value);
            SendMessageCommand.RaiseCanExecuteChanged();
        }
    }

    private void ExecuteConnectToServer()
    {
        _server.ConnectToServer(Username);
        Username = string.Empty;
    }

    private bool CanExecuteConnectToServer()
    {
        return string.IsNullOrEmpty(Username) == false;
    }

    private void ExecuteSendMessage()
    {
        _server.SendMessageToServer(Message);
        Message = string.Empty;
    }

    private bool CanExecuteSendMessage()
    {
        return string.IsNullOrEmpty(Message) == false;
    }

    private void UserConnectionEvent()
    {
        var user = new UserModel()
        {
            Username = _server.PacketReader.ReadMessage(),
            UID = _server.PacketReader.ReadMessage()
        };

        if(!Users.Any(x => x.UID == user.UID))
        {
            Application.Current.Dispatcher.Invoke(() => { Users.Add(user); });
        }

    }

    private void UserDisconnectEvent()
    {
        var uid = _server.PacketReader.ReadMessage();
        var user = Users.Where(x => x.UID == uid).FirstOrDefault();
        if(user != null)
        {
            Application.Current.Dispatcher.Invoke(() => { Users.Remove(user); });
        }
    }

    private void MessageRecievedEvent()
    {
        Application.Current.Dispatcher.Invoke(() => 
        { 
            Messages.Add(_server.PacketReader.ReadMessage()); 
        });
    }

}