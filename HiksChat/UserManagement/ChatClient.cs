using HiksChat.ChatManagement;
using HiksChat.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HiksChat.UserManagement
{
    public class ChatClient
    {
        public User User { get; set; }
        public TranslationService.TranslationService translationService { get; set; }
        public List<string> messageHistory { get; set; }
        public string preferredLanguage { get; set; }

        public ChatClient(User user)
        {
            this.User = user;
            messageHistory = new List<string>();
        }

        public void SetPreferredLanguage(string language)
        {
            preferredLanguage = language;
        }

        public void SendMessage(string message)
        {
            // Implement send message logic
            if (!string.IsNullOrEmpty(message))
            {
                messageHistory.Add(message);
            }
            else
            {
                Console.WriteLine("Cannot send an empty message.");
            }
        }

        public string ReceiveMessage()
        {
            // Implement receive message logic
            if (messageHistory.Count > 0)
            {
                string lastMessage = messageHistory.Last();
                messageHistory.Remove(lastMessage);
                return lastMessage;
            }
            else
            {
                return null;
            }
        }

        public List<string> ViewChatHistory()
        {
            return messageHistory;
        }

        public void ClearChatHistory()
        {
            messageHistory.Clear();
            Console.WriteLine("Chat history cleared");
        }

        public void ProcessCommand(string command)
        {
            string[] commandParts = command.Split(' ');

            switch (commandParts[0].ToLower())
            {
                case "/help":
                    Console.WriteLine("=== Help Menu ===");
                    Console.WriteLine("Available commands:");
                    Console.WriteLine("/help       - Display this help menu");
                    Console.WriteLine("/language   - Change preferred language");
                    Console.WriteLine("/history    - View chat history");
                    Console.WriteLine("/settings   - Shows your settings");
                    Console.WriteLine("/ping       - Shows your ping");
                    break;

                case "/language":
                    if (commandParts.Length > 1)
                    {
                        SetPreferredLanguage(commandParts[1]);
                        Console.WriteLine($"Preferred language changed to {commandParts[1]}.");
                    }
                    else
                    {
                        Console.WriteLine("Please provide a language. Example: /language English");
                    }
                    break;
                case "/settings":

                    break;
                case "/history":
                    ViewChatHistory();
                    break;

                case "/ping":
                    int ping = CalculatePing();
                    Console.WriteLine($"Ping to server: {ping} ms");
                    Console.WriteLine("Note: Lower ping values indicate faster response times.");
                    break;

                default:
                    Console.WriteLine("Unknown command. Type /help to see the available commands.");
                    break;
            }
        }

        public int CalculatePing()
        {
            using (Ping pingSender = new Ping())
            {
                string serverAddress = "gso-koeln.de";

                try
                {
                    PingReply reply = pingSender.Send(serverAddress);
                    if (reply.Status == IPStatus.Success)
                    {
                        return (int)reply.RoundtripTime;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (PingException)
                {
                    return -1;
                }
            }
        }
    }
}

