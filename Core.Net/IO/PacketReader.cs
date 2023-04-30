using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Core.Net.IO;

public class PacketReader : BinaryReader
{
    private NetworkStream _networkStream;

    public PacketReader(NetworkStream networkStream) : base(networkStream)
    {
        _networkStream = networkStream;
    }

    public string ReadMessage()
    {
        byte[] messageBuffer;
        var length = ReadInt32();
        messageBuffer = new byte[length];
        _networkStream.Read(messageBuffer, 0x00, length);

        var message = Encoding.ASCII.GetString(messageBuffer);
        return message;
    }
}