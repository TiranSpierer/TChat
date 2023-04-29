using System.Collections.Generic;
using System.Linq;

namespace Core.DataModels;

public class Chat
{
    public Contact Contact { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public Message LastMessage => Messages.LastOrDefault();
}