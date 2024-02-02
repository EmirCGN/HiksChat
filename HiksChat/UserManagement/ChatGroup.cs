using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiksChat.UserManagement
{
    public class ChatGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ChatClient> members { get; set; }

        public void AddMember(ChatClient member)
        {
            // Implement add member logic
        }

        public void RemoveMember(ChatClient member)
        {
            // Implement remove member logic
        }

        public void SaveToDatabase()
        {
            // Implement save to database logic
        }

        public ChatGroup LoadFromDatabase(int groupId)
        {
            // Implement load from database logic
            return new ChatGroup();
        }

        public void SaveMessageToDatabase(string sender, string content)
        {
            // Implement save message to database logic
        }

        public List<string> GetChatHistoryFromDatabase()
        {
            // Implement get chat history from database logic
            return new List<string>();
        }

        public int GetGroupId()
        {
            // Implement get group Id logic
            return 0;
        }
    }
}
