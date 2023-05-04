using Core.DataModels;

namespace Core.StateMachine;

public class TChatContext
{
    public User CurrentUser { get; set; } 
    public Chat CurrentChat { get; set; }
}