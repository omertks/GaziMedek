using MessageService.Db;
using MessageService.Dtos;
using MessageService.Models;
using MessageService.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;

namespace MessageService.Services
{
    public class ChatService : IChatService
    {
        private readonly MessageServiceDbContext _context;
        private readonly UserClient _userClient;

        public ChatService(MessageServiceDbContext context, UserClient userClient)
        {
            _context = context;
            _userClient = userClient;
        }

        public async Task<Chat> CreateChatAsync(List<long> userIds)
        {
            // Şuanlık burası yok
            //foreach (var userId in userIds)
            //{
            //    if (!await _userClient.IsValidUserAsync(userId))
            //        throw new Exception($"Invalid user ID: {userId}");
            //}

            var chat = new Chat();
            foreach (var userId in userIds.Distinct())
            {
                chat.Participants.Add(new ChatParticipant { UserId = userId });
            }

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return chat;
        }

        public async Task AddParticipantAsync(int chatId, long userId)
        {
            var chat = await _context.Chats
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(c => c.Id == chatId)
                ?? throw new Exception("Chat not found");

            if (!await _userClient.IsValidUserAsync(userId))
                throw new Exception("Invalid user ID");

            if (!chat.Participants.Any(p => p.UserId == userId))
            {
                chat.Participants.Add(new ChatParticipant { ChatId = chatId, UserId = userId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveParticipantAsync(int chatId, long userId)
        {
            var participant = await _context.ChatParticipants
                .FirstOrDefaultAsync(p => p.ChatId == chatId && p.UserId == userId)
                ?? throw new Exception("Participant not found");

            _context.ChatParticipants.Remove(participant);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResultChatDto>> GetChatsByUserIdAsync(long userId)
        {
            var chats = await _context.Chats
                .Include(c => c.Participants)
                .Where(c => c.Participants.Any(p => p.UserId == userId))
                .ToListAsync();

            List<ResultChatDto> rs = new List<ResultChatDto>();

            foreach (var chat in chats)
            {
                List<long> ids = new List<long>();

                foreach (var item in chat.Participants)
                {
                    ids.Add(item.UserId);
                }

                rs.Add(new ResultChatDto()
                {
                    Id = chat.Id,
                    userIds = ids
                });
            }

            return rs;

        }

        public async Task<List<Message>> GetMessagesAsync(int chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
    }

}
