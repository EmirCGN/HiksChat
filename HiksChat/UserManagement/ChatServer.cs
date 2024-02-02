using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiksChat.UserManagement
{
    public class ChatServer
    {
        public List<ChatGroup> groups { get; set; }
        public List<ChatClient> users { get; set; }

        public ChatGroup CreateGroup(string groupName)
        {
            // Implement create group logic
            return new ChatGroup();
        }

        public void JoinGroup(ChatClient client, string groupName)
        {
            // Implement join group logic
        }

        public ChatClient RegisterUser(string username, string language)
        {
            // Implement register user logic
            return new ChatClient();
        }

        public ChatClient GetUserById(int userId)
        {
            // Implement get user by Id logic
            return new ChatClient();
        }

        public ChatClient GetUserByUsername(string username)
        {
            // Implement get user by username logic
            return new ChatClient();
        }

        public ChatGroup GetGroupByName(string groupName)
        {
            // Implement get group by name logic
            return new ChatGroup();
        }

        public ChatGroup GetGroupById(int groupId)
        {
            // Implement get group by Id logic
            return new ChatGroup();
        }

        public List<ChatClient> GetAllUsers()
        {
            // Implement get all users logic
            return new List<ChatClient>();
        }

        public List<ChatGroup> GetAllGroups()
        {
            // Implement get all groups logic
            return new List<ChatGroup>();
        }

        public void RemoveUser(int userId)
        {
            // Implement remove user logic
        }

        public void RemoveGroup(int groupId)
        {
            // Implement remove group logic
        }

        public void ChangeUsername(int userId, string newUsername)
        {
            // Implement change username logic
        }

        public void ChangeLanguage(int userId, string newLanguage)
        {
            // Implement change language logic
        }
    }
}
