using ChatService;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IChatClient _client;

    public MainWindow()
    {
        InitializeComponent();
        _client = new ChatClient("localhost", 8888);
        _client.MessageReceived += OnMessageReceived;

        SendButton.Click += OnSendButtonClick;
    }

    private async void OnSendButtonClick(object sender, RoutedEventArgs e)
    {
        var message = MessageTextBox.Text;
        await _client.SendMessageAsync(message);
        MessageTextBox.Text = "";
    }

    private void OnMessageReceived(string message)
    {
        Dispatcher.Invoke(() =>
        {
            MessagesListBox.Items.Add(message);
            MessagesListBox.ScrollIntoView(MessagesListBox.Items[MessagesListBox.Items.Count - 1]);
        });
    }
}
