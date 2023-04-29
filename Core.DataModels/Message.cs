using System;

namespace Core.DataModels;

public class Message
{
    public int Id { get; }
    public string Text { get; set; }
    public string FromNumber { get; set; }
    public string ToNumber { get; set; }
    public DateTime TimeSent { get; } = DateTime.Now;

}