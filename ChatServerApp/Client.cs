using Core.Net.IO;
using System.Net.Sockets;

namespace ChatServerApp;

public class Client
{
    private readonly PacketReader _packetReader;

    public Client(TcpClient client)
    {
        ClientSocket = client;
        UID = Guid.NewGuid();
        _packetReader = new PacketReader(ClientSocket.GetStream());

        var opCode = _packetReader.ReadByte();
        Username = _packetReader.ReadMessage();

        Task.Run(() => Process());
    }

    public string Username { get; set; }
    public Guid UID { get; set; }
    public TcpClient ClientSocket { get; set; }

    private void Process()
    {
        while (true)
        {
            try
            {
                var opcode = _packetReader.ReadByte(); 

                switch(opcode)
                {
                    case 5:
                        var message = _packetReader.ReadMessage();
                        Console.WriteLine($"[{DateTime.Now}]: Message recieved! {message}");
                        ChatServer.BroadcastMessage(message);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{UID}]: Disconnected");
                ChatServer.BroadcastDisconnect(UID.ToString());
                ClientSocket.Close();
            }
        }
    }
}