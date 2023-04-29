using Core.Interfaces;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System;
using Core.DataModels;
using System.Threading.Tasks;

namespace ChatService;

public class LocalChatService : IChatService
{
    private const int Port = 1234;
    private readonly IPAddress _ipAddress = IPAddress.Parse("127.0.0.1");
    private TcpClient _client;
    private NetworkStream _stream;
    private CancellationTokenSource _cancellationTokenSource;

    public event Action<string> MessageReceived;

    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        _client = new TcpClient();
        await _client.ConnectAsync(_ipAddress, Port);

        _stream = _client.GetStream();
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        await Task.Run(Listen, _cancellationTokenSource.Token);
    }

    public void Disconnect()
    {
        _cancellationTokenSource.Cancel();
        _stream.Close();
        _client.Close();
    }

    public async Task SendMessageAsync(Message message)
    {
        var bytes = Encoding.UTF8.GetBytes(message.Text);
        await _stream.WriteAsync(bytes, 0, bytes.Length);
    }

    private async Task Listen()
    {
        var buffer = new byte[1024];

        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            try
            {
                var bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, _cancellationTokenSource.Token);

                if (bytesRead > 0)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    MessageReceived?.Invoke(message);
                }
            }
            catch (Exception ex)
            {
                // Handle network exception
            }
        }
    }

}