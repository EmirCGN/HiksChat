using HiksChat.Database;
using Microsoft.Data.SqlClient;
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
        public List<ChatClient> Members { get; set; }

        public void AddMember(ChatClient member)
        {
            // Implement add member logic
            Members.Add(member);
            Console.WriteLine($"Member {member.Name} added to group {Name}.");
        }

        public void RemoveMember(ChatClient member)
        {
            // Implement remove member logic
            Members.Remove(member);
            Console.WriteLine($"Member {member.Name} removed from group {Name}.");
        }

        public void SaveToDatabase()
        {
            using (SqlConnection connection = new SqlConnection(DatabaseManager.connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Groups (GroupName) VALUES (@GroupName)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupName", Name);
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine($"Group {Name} saved to database.");
        }

        public static ChatGroup LoadFromDatabase(int groupId)
        {
            ChatGroup group = new ChatGroup();
            using (SqlConnection connection = new SqlConnection(DatabaseManager.connectionString))
            {
                connection.Open();
                string query = "SELECT GroupName FROM Groups WHERE GroupId = @GroupId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupId", groupId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            group.Name = reader.GetString(0);
                        }
                    }
                }
            }
            Console.WriteLine($"Group {group.Name} loaded from database.");
            return group;
        }

        public void SaveMessageToDatabase(string sender, string content)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseManager.connectionString))
            {
                connection.Open();
                string query = "INSERT INTO GroupMessages (GroupId, Sender, Content) VALUES (@GroupId, @Sender, @Content)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupId", Id);
                    command.Parameters.AddWithValue("@Sender", sender);
                    command.Parameters.AddWithValue("@Content", content);
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine($"Message from {sender} saved to database for group {Name}.");
        }

        public List<string> GetChatHistoryFromDatabase()
        {
            List<string> chatHistory = new List<string>();
            using (SqlConnection connection = new SqlConnection(DatabaseManager.connectionString))
            {
                connection.Open();
                string query = "SELECT Content FROM GroupMessages WHERE GroupId = @GroupId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupId", Id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chatHistory.Add(reader.GetString(0));
                        }
                    }
                }
            }
            Console.WriteLine($"Chat history retrieved from database for group {Name}.");
            return chatHistory;
        }

        public int GetGroupId()
        {
            Console.WriteLine($"Group ID: {Id}");
            return Id;
        }
    }
}
