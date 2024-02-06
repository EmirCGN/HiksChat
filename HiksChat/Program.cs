using HiksChat.Database;
using HiksChat.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiksChat
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new DatabaseManager())
            {
                dbContext.InitializeDatabase();
            }
                ConsoleUI.RunMainMenu();
        }
    }
}
