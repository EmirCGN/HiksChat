using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using HiksChat.ChatManagement;

namespace HiksChat.Database
{
    public class DatabaseManager : DbContext
    {
        public static string connectionString { get; set; } = @"Data Source=C:\Users\korog\source\repos\EmirCGN\HiksChat\HiksChat\Database\HiksChat.db";

        public DbSet<User> Users { get; set; } = null;

        public void InitializeDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                CreateTablesIfNotExists(connection);
            }
        }

        public void CreateTablesIfNotExists(SqliteConnection connection)
        {
            string usersTableQuery = @"
            CREATE TABLE IF NOT EXISTS Users (
                Username TEXT PRIMARY KEY,
                Password TEXT NOT NULL,
                Language TEXT NOT NULL
            );";
            using (SqliteCommand command = new SqliteCommand(usersTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            string groupsTableQuery = @"
            CREATE TABLE IF NOT EXISTS Groups (
                GroupId INTEGER PRIMARY KEY AUTOINCREMENT,
                GroupName TEXT NOT NULL
            );";
            using (SqliteCommand command = new SqliteCommand(groupsTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public bool CheckLogin(string username, string password)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        long count = (long)result; // Hier den Rückgabewert als long interpretieren
                        return count > 0;
                    }
                    return false;
                }
            }
        }



        public void SaveMessage(string sender, string receiver, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Messages (Sender, Receiver, Content) VALUES (@Sender, @Receiver, @Content)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Sender", sender);
                    command.Parameters.AddWithValue("@Receiver", receiver);
                    command.Parameters.AddWithValue("@Content", content);
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task<List<string>> GetChatHistoryFromDatabaseAsync(int groupId)
        {
            List<string> chatHistory = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT Content FROM GroupMessages WHERE GroupId = @GroupId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GroupId", groupId);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                chatHistory.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                Console.WriteLine($"Chat history retrieved from database for group {groupId}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving chat history: {ex.Message}");
            }
            return chatHistory;
        }

        public async Task<int> GetGroupIdAsync()
        {
            int groupId = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "SELECT Id FROM Groups WHERE Name = @GroupName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GroupName", "YourGroupName");
                        object result = await command.ExecuteScalarAsync();
                        if (result != null)
                        {
                            groupId = Convert.ToInt32(result);
                        }
                    }
                }
                Console.WriteLine($"Group ID: {groupId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving group ID: {ex.Message}");
            }
            return groupId;
        }


        public void SaveUser(string username, string password, string language)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Username, Password, Language) VALUES (@Username, @Password, @Language)";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Language", language);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SaveGroup(string groupName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Groups (GroupName) VALUES (@GroupName)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupName", groupName);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Users WHERE UserId = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveGroup(int groupId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Groups WHERE GroupId = @GroupId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupId", groupId);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
