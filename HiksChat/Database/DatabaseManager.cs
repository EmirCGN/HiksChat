using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiksChat.Database
{
    public class DatabaseManager
    {
        public string connectionString { get; set; }

        public void InitializeDatabase()
        {
            // Implement initialize database logic
        }

        public void SaveMessage(string sender, string receiver, string content)
        {
            // Implement save message logic
        }

        public List<string> GetChatHistory(string user)
        {
            // Implement get chat history logic
            return new List<string>();
        }

        public void SaveUser(string username, string language)
        {
            // Implement save user logic
        }

        public void SaveGroup(string groupName)
        {
            // Implement save group logic
        }

        public void RemoveUser(int userId)
        {
            // Implement remove user logic
        }

        public void RemoveGroup(int groupId)
        {
            // Implement remove group logic
        }
    }
}
