using Core.DataModels;

namespace Core.State;

public class TChatContext
{
    public User CurrentUser { get; set; } 
    public Chat CurrentChat { get; set; }
}