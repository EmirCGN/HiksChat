using HiksChat.ChatManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiksChat.UserManagement
{
    public class ChatServer
    {
        public List<ChatGroup> Groups { get; }
        public List<ChatClient> Users { get; }

        public ChatServer()
        {
            Groups = new List<ChatGroup>();
            Users = new List<ChatClient>();
        }

        #region Group Management

        public ChatGroup CreateGroup(string groupName)
        {
            var group = new ChatGroup { Name = groupName };
            Groups.Add(group);
            return group;
        }

        public void JoinGroup(ChatClient client, string groupName)
        {
            var group = GetGroupByName(groupName);
            if (group != null)
            {
                group.AddMember(client);
            }
            else
            {
                Console.WriteLine($"Group '{groupName}' does not exist");
            }
        }

        public ChatGroup GetGroupByName(string groupName)
        {
            return Groups.FirstOrDefault(group => group.Name == groupName);
        }

        public ChatGroup GetGroupById(int groupId)
        {
            return Groups.FirstOrDefault(group => group.GroupId == groupId);
        }

        public void RemoveGroup(int groupId)
        {
            var group = GetGroupById(groupId);
            if (group != null)
            {
                Groups.Remove(group);
            }
            else
            {
                Console.WriteLine($"Group with ID {groupId} does not exist");
            }
        }

        #endregion

        #region User Management
        public ChatClient GetUserById(int userId)
        {
            return Users.FirstOrDefault(user => user.User.UserId == userId);
        }

        public ChatClient GetUserByUsername(string username)
        {
            return Users.FirstOrDefault(user => user.User.Username == username);
        }

        public void RemoveUser(int userId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                Users.Remove(user);
            }
            else
            {
                Console.WriteLine($"User with ID {userId} does not exist");
            }
        }

        public void ChangeUsername(int userId, string newUsername)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.User.Username = newUsername;
            }
            else
            {
                Console.WriteLine($"User with ID {userId} does not exist");
            }
        }

        public void ChangeLanguage(int userId, string newLanguage)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.User.Language = newLanguage;
            }
            else
            {
                Console.WriteLine($"User with ID {userId} does not exist.");
            }
        }

        #endregion

        #region Chat Management

        public void CreateChat(ChatClient user1, ChatClient user2)
        {
            var group = new ChatGroup { Name = $"{user1.User.Username}_{user2.User.Username}" };
            group.AddMember(user1);
            group.AddMember(user2);
            Groups.Add(group);
        }

        #endregion
    }
}
