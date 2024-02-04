using HiksChat.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiksChat.ChatManagement
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Language { get; set; }
        public string PreferredLanguage { get; set; }
        public List<ChatGroup> Groups { get; set; }

        public void RegisterUser(string username, string language)
        {
            this.Username = username;
            this.Language = language;
        }

        public List<ChatGroup> GetGroups()
        {
            return Groups;
        }
    }
}
