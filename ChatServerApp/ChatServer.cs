using System.Net.Sockets;
using System.Net;
using System.Text;
using Core.Net.IO;

namespace ChatServerApp;

public static class ChatServer
{
    private static TcpListener _listener;
    private static List<Client> _users = new();

    static ChatServer()
    {
        _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
        _listener.Start();
        Console.WriteLine($"Server started listening on port {7891}...");
    }

    public static async Task StartAsync()
    {
        while (true)
        {
            var clientTask = _listener.AcceptTcpClientAsync();
            var completedTask = await Task.WhenAny(clientTask, Task.Delay(TimeSpan.FromSeconds(1)));

            if (completedTask != clientTask)
            {
                continue; // timeout, check for new clients
            }

            var client = new Client(clientTask.Result);

            _users.Add(client);

            Console.WriteLine($"[{DateTime.Now}]: Client Connected: [{client.Username}] [{client.ClientSocket.Client.RemoteEndPoint}]");

            _ = HandleClientAsync(client); // start a new task to handle the client
            BroadcastConnection();
        }
    }

    public static void BroadcastMessage(string message)
    {
        foreach (var user in _users)
        {
            var messagePacket = new PacketBuilder(5, message);
            user.ClientSocket.Client.Send(messagePacket.GetPacketBytes());
        }
    }

    public static void BroadcastDisconnect(string uid)
    {
        var disconnectedUser = _users.Where(x => x.UID.ToString() == uid).FirstOrDefault();
        if (disconnectedUser != null)
        {
            _users.Remove(disconnectedUser);
        }

        foreach (var user in _users)
        {
            var broadcastPacket = new PacketBuilder(10, uid);
            user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
        }

        BroadcastMessage($"[{disconnectedUser} Disconnected]");
    }

    private static async Task HandleClientAsync(Client client)
    {
        var stream = client.ClientSocket.GetStream();
        var reader = new StreamReader(stream, Encoding.UTF8);

        while (client.ClientSocket.Connected)
        {
            try
            {
                var message = await reader.ReadLineAsync();

                if (string.IsNullOrEmpty(message) == false)
                {
                    Console.WriteLine($"Received message from {client.Username}: {message}");

                    foreach (var otherClient in _users.Where(c => c.ClientSocket != client.ClientSocket))
                    {
                        var otherStream = otherClient.ClientSocket.GetStream();
                        var writer = new StreamWriter(otherStream, Encoding.UTF8) { AutoFlush = true };
                        await writer.WriteLineAsync($"From {client.Username}: {message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                break;
            }
        }

        Console.WriteLine($"Client disconnected: {client.Username}");
        _users.Remove(client);
    }

    private static void BroadcastConnection()
    {
        foreach(var user in _users)
        {
            foreach(var usr in _users)
            {
                var broadcastPacket = new PacketBuilder(0x01, new string[]{ usr.Username, usr.UID.ToString() });
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
            }
        }
    }


}