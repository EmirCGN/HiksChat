using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiksServer.Models
{
    public class Messages
    {
        public int ID { get; set; }
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
        public string Inhalt { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual ICollection<Messages> Answer { get; set; }
    }
}
