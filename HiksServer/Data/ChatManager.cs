using System;
using System.Linq;
using System.Threading;
using HiksServer.Models;

namespace HiksServer.Data
{
    public class ChatManager
    {
        private readonly ChatDbContext _context;

        public ChatManager()
        {
            _context = new ChatDbContext();
            _context.Database.EnsureCreated(); 
        }

        public void AddUser(string username, string password, int nummer)
        {
            var user = new User { Username = username, Password = password, Nummer = nummer };
            _context.Users.Add(user);
            _context.SaveChanges();
            Console.WriteLine($"User {username} has been added.");
        }

        public void CreateChat(string chatName, params User[] chatMembers)
        {
            var chat = new Chat { Name = chatName, Users = chatMembers.Select(u => new ChatUser { User = u }).ToList() };
            _context.Chats.Add(chat);
            _context.SaveChanges();
            Console.WriteLine($"Chat {chatName} has been created.");
        }

        public void SendMessage(User sender, Chat chat, string message)
        {
            var newMessage = new Messages { Chat = chat, Inhalt = message, Timestamp = DateTime.Now };
            chat.Messages.Add(newMessage);
            _context.SaveChanges();
            Console.WriteLine($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {sender.Username} im Chat \"{chat.Name}\" hat gesendet: \"{message}\"");
        }


        public Chat GetChatById(int chatId)
        {
            return _context.Chats.Find(chatId);
        }
    }
}
