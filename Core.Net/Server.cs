using Core.Net.IO;
using System.Net.Sockets;

namespace Core.Net;

public class Server
{
    private TcpClient _tcpClient;

    public Server()
    {
        _tcpClient = new TcpClient();
    }

    public PacketReader PacketReader { get; private set; }

    public event Action ConnectionEvent;
    public event Action MessageRecievedEvent;
    public event Action UserDisconnectEvent;

    public void ConnectToServer(string username)
    {
        if(_tcpClient.Connected == false)
        {
            _tcpClient.Connect("127.0.0.1", 7891);
            PacketReader = new PacketReader(_tcpClient.GetStream());

            if(string.IsNullOrEmpty(username) == false)
            {
                var connectPacket = new PacketBuilder(0, username);
                _tcpClient.Client.Send(connectPacket.GetPacketBytes());
                ReadPackets();
            }
        }
    }

    public void ReadPackets()
    {
        Task.Run(() =>
        {
            while (true)
            {
                var opCode = PacketReader.ReadByte();
                switch (opCode)
                {
                    case 0:
                        break;

                    case 1:
                        ConnectionEvent?.Invoke();
                        break;

                    case 5:
                        MessageRecievedEvent?.Invoke();
                        break;

                    case 10:
                        UserDisconnectEvent?.Invoke();
                        break;

                    default:
                        throw new Exception("incorrect operation code");
                }
            }
        });
    }

    public void SendMessageToServer(string message)
    {
        var messagePacket = new PacketBuilder(0x05, message);
        _tcpClient.Client.Send(messagePacket.GetPacketBytes());
    }
}