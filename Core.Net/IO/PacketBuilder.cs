using System.IO;
using System.Text;
using System;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Core.Net.IO;

public class PacketBuilder
{
    private readonly MemoryStream _ms = new();
    private bool _opFlag;
    private bool _msgFlag;

    public PacketBuilder()
    {

    }

    public PacketBuilder(byte opCode, params string[] messages)
    {
        WriteMessages(opCode, messages);
    }

    public void WriteMessages(byte opCode, params string[] messages)
    {
        WriteOpCode(opCode);
        foreach (var msg in messages)
        {
            WriteMessage(msg);
        }
    }

    public byte[] GetPacketBytes()
    {
        return _ms.ToArray();
    }

    private void WriteOpCode(byte opcode)
    {
        if (_opFlag)
            throw new Exception("Operation Code already set");

        _ms.WriteByte(opcode);
        _opFlag = true;
    }

    private void WriteMessage(string message)
    {
        if (_opFlag == false)
            throw new Exception("Operation Code not set");

        _ms.Write(BitConverter.GetBytes(message.Length));
        _ms.Write(Encoding.ASCII.GetBytes(message));
    }

    
}