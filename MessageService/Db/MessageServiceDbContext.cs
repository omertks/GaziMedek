using MessageService.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MessageService.Db
{
    public class MessageServiceDbContext : DbContext
    {
        public MessageServiceDbContext(DbContextOptions<MessageServiceDbContext> options)
                    : base(options) { }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatParticipant> ChatParticipants { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatParticipant>()
                .HasKey(cp => new { cp.ChatId, cp.UserId });

            modelBuilder.Entity<ChatParticipant>()
                .HasOne(cp => cp.Chat)
                .WithMany(c => c.Participants)
                .HasForeignKey(cp => cp.ChatId);

            modelBuilder.Entity<Message>()
    .HasOne(m => m.Chat)
    .WithMany(c => c.Messages)
    .HasForeignKey(m => m.ChatId);

            base.OnModelCreating(modelBuilder);
        }
    }

}
