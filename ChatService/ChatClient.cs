

using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System;
using Core.Interfaces;
using System.Net;
using System.Threading;

namespace ChatService;

public class ChatClient : IChatClient
{
    private TcpClient _client;
    private StreamReader _reader;
    private StreamWriter _writer;

    public event Action<string> MessageReceived;

    public ChatClient(string ipAddress, int port)
    {
        _ = ConnectAsync(ipAddress, port);
    }

    public async Task SendMessageAsync(string message)
    {
        await _writer.WriteLineAsync(message);
    }

    private async Task ListenAsync()
    {
        while (_client.Connected)
        {
            var message = await _reader.ReadLineAsync();
            if (string.IsNullOrEmpty(message) == false)
            {
                MessageReceived?.Invoke(message);
            }
        }

    }

    private async Task ConnectAsync(string ipAddress, int port)
    {
        //_client = new TcpClient(ipAddress, port);

        _client = new TcpClient();
        _client.ExclusiveAddressUse = false;
        await _client.ConnectAsync(ipAddress, port);


        var stream = _client.GetStream();
        _reader = new StreamReader(stream, Encoding.UTF8);
        _writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

        _ = Task.Run(ListenAsync);
    }

}
