using System.Collections.Generic;

namespace Core.DataModels;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Contact> Contacts { get; set; }
    public ICollection<Chat> Chats { get; set; }

}