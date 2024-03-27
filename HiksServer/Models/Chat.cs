using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiksServer.Models
{
    public class Chat
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ChatUser> Users { get; set; }
        public virtual ICollection<Messages> Messages { get; set; }

        public Chat()
        {
            Users = new List<ChatUser>();
            Messages = new List<Messages>();
        }
    }
}
