using MessageService.Db;
using MessageService.Models;
using MessageService.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;

namespace MessageService.Services
{
    public class MessageServiceC : IMessageService
    {
        private readonly MessageServiceDbContext _context;
        private readonly UserClient _userClient;

        public MessageServiceC(MessageServiceDbContext context, UserClient userClient)
        {
            _context = context;
            _userClient = userClient;
        }

        public async Task<Message> SendMessageAsync(int chatId, long senderId, string content)
        {
            // bu kısmı aktif et daha sonra

            //if (!await _userClient.IsValidUserAsync(senderId))
            //    throw new Exception("Invalid user ID.");

            // Buraları daha sonra kontrol et

            //var chat = await _context.Chats
            //    .Include(c => c.Participants)
            //    .FirstOrDefaultAsync(c => c.Id == chatId);

            //if (chat == null)
            //    throw new Exception("Chat not found.");

            //if (!chat.Participants.Any(p => p.UserId == senderId))
            //    throw new Exception("User is not a participant in this chat.");

            var message = new Message
            {
                ChatId = chatId,
                SenderId = senderId,
                Content = content
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }
    }


}
