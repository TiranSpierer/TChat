using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ChatServerApp;

public class ChatServer
{
    private readonly TcpListener _listener;
    private readonly List<TcpClient> _clients = new List<TcpClient>();

    public ChatServer(int port)
    {
        _listener = new TcpListener(IPAddress.Any, port);
        _listener.Start();
        Console.WriteLine($"Server started listening on port {port}...");
    }

    public async Task StartAsync()
    {
        while (true)
        {
            var client = await _listener.AcceptTcpClientAsync();
            _clients.Add(client);
            Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

            await Task.Run(() => HandleClientAsync(client));
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        var stream = client.GetStream();
        var reader = new StreamReader(stream, Encoding.UTF8);

        while (client.Connected)
        {
            var message = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(message)) continue;

            Console.WriteLine($"Received message from {client.Client.RemoteEndPoint}: {message}");

            foreach (var otherClient in _clients.Where(c => c != client))
            {
                var otherStream = otherClient.GetStream();
                var writer = new StreamWriter(otherStream, Encoding.UTF8) { AutoFlush = true };
                await writer.WriteLineAsync($"From {client.Client.RemoteEndPoint}: {message}");
            }
        }

        Console.WriteLine($"Client disconnected: {client.Client.RemoteEndPoint}");
        _clients.Remove(client);
    }
}
