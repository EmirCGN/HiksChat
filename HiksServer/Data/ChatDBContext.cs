using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HiksServer.Models;

namespace HiksServer.Data
{
    public class ChatDbContext : DbContext
    {
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Chat> Chats { get; set; }
        public DbSet<Models.Messages> Messages { get; set; }
        public DbSet<Models.ChatUser> ChatUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Konfigurieren Sie hier Ihre Datenbankverbindung
            optionsBuilder.UseSqlServer("C:\\Users\\korog\\Source\\Repos\\HiksChat\\HiksChat\\Database\\HiksChat.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ChatUser>()
                .HasKey(cu => new { cu.UserId, cu.ChatId }); //Configures the primary keys for the ChatUser relationship table.

            modelBuilder.Entity<Models.ChatUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.Chats)
                .HasForeignKey(cu => cu.UserId); //Configures the relationship between User and ChatUser.

            modelBuilder.Entity<Models.ChatUser>()
                .HasOne(cu => cu.Chat)
                .WithMany(c => c.Users)
                .HasForeignKey(cu => cu.ChatId);//Configures the relationship between Chat and ChatUser.

            modelBuilder.Entity<Models.Messages>()
                .HasOne(n => n.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(n => n.ChatId); //Configures the relationship between messages and chat.


            modelBuilder.Entity<Models.Messages>()
                .HasMany(n => n.Answer)
                .WithOne(); //Configures the relationship between messages and itself for the responses.
        }
    }
}
