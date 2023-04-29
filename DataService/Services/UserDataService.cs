

using Core.DataModels;
using Core.Interfaces.DataServices;
using System.Collections.Generic;
using System.Linq;

namespace DataService.Services;

public class UserDataService : IUserDataService
{

    public UserDataService()
    {
        
    }

    public void AddUser(User user)
    {
        throw new System.NotImplementedException();
    }

    public bool AuthenticateUser(string userName, string password)
    {
        throw new System.NotImplementedException();
    }

    public User GetUser(string userName)
    {
        var tNum = "0587116155";
        var mNum = "0533326552";

        var contacts = new List<Contact>
        {
            new Contact() { Name = "Moran", Phone = "0533326552" },
            new Contact() { Name = "Tamir", Phone = "0547618344" }
        };

        var messages = new List<Message>
        {
            new Message() {Text = "Hi", FromNumber = tNum, ToNumber = mNum},
            new Message() {Text = "I Love you", FromNumber = tNum, ToNumber = mNum},
            new Message() {Text = "No", FromNumber = mNum, ToNumber = tNum},
            new Message() {Text = "Why not?", FromNumber = tNum, ToNumber = mNum},
            new Message() {Text = "You're too hairy", FromNumber = mNum, ToNumber = tNum},
            new Message() {Text = "What's wrong with hair?", FromNumber = tNum, ToNumber = mNum},
            new Message() {Text = "It's gross", FromNumber = mNum, ToNumber = tNum},
            new Message() {Text = "Well, I think it's manly", FromNumber = tNum, ToNumber = mNum},
            new Message() {Text = "You're not a man, you're a gorilla", FromNumber = mNum, ToNumber = tNum},
            new Message() {Text = "That's not nice", FromNumber = tNum, ToNumber = mNum},
            new Message() {Text = "I'm just being honest", FromNumber = mNum, ToNumber = tNum},
            new Message() {Text = "Fine, I'll shave", FromNumber = tNum, ToNumber = mNum},
            new Message() {Text = "Don't do it for me, do it for yourself", FromNumber = mNum, ToNumber = tNum},
            new Message() {Text = "I don't care, I just want you to love me", FromNumber = tNum, ToNumber = mNum},
            new Message() {Text = "I love you, even if you're a little hairy", FromNumber = mNum, ToNumber = tNum},
            new Message() {Text = "A little hairy?", FromNumber = tNum, ToNumber = mNum},
            new Message() {Text = "Okay, a lot hairy", FromNumber = mNum, ToNumber = tNum},
            new Message() {Text = "Thanks a lot", FromNumber = tNum, ToNumber = mNum},
            new Message() {Text = "I mean, you're still cute", FromNumber = mNum, ToNumber = tNum},
            new Message() {Text = "Aww, thanks", FromNumber = tNum, ToNumber = mNum},
        };

        var chats = new List<Chat>
        {
            new Chat() { Messages = messages, Contact = contacts[0]},
            new Chat() { Contact = contacts[1]}
        };

        return new User
        {
            Username = "Tiran",
            Password = "123",
            PhoneNumber = "0587116155",
            Contacts = contacts,
            Chats = chats
        };
    }

    public ICollection<User> GetUsers()
    {
        throw new System.NotImplementedException();
    }
}