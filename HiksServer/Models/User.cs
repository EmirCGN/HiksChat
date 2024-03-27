using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiksServer.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Nummer { get; set; }
        public virtual ICollection<ChatUser> Chats { get; set; }

        public User()
        {
            Chats = new List<ChatUser>();
        }
    }
}
