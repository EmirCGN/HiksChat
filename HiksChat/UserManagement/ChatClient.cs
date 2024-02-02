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
        public TranslationService translationService { get; set; }
        public List<string> messageHistory { get; set; }
        public string preferredLanguage { get; set; }

        public void SetPreferredLanguage(string language)
        {
            preferredLanguage = language;
        }

        public void SendMessage(string message)
        {
            // Implement send message logic
        }

        public void ReceiveMessage(string message)
        {
            // Implement receive message logic
        }

        public void ViewChatHistory()
        {
            // Implement view chat history logic
        }

        public void ProcessCommand(string command)
        {
            // Implement process command logic
        }
    }
}
