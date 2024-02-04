using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiksChat.UserManagement
{
    public class ChatClient
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public TranslationService.TranslationService translationService { get; set; }
        public List<string> messageHistory { get; set; }
        public string preferredLanguage { get; set; }

        public void SetPreferredLanguage(string language)
        {
            preferredLanguage = language;
        }

        public void SendMessage(string message)
        {
            // Implement send message logic
            messageHistory.Add(message);
        }

        public string ReceiveMessage()
        {
            // Implement receive message logic
            return messageHistory.Last();
        }

        public List<string> ViewChatHistory()
        {
            return messageHistory;
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

                case "/history":
                    ViewChatHistory();
                    break;

                default:
                    Console.WriteLine("Unknown command. Type /help to see the available commands.");
                    break;
            }
        }
    }
}

