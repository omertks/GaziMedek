using MessageService.Dtos;
using MessageService.Models;

namespace MessageService.Services.Abstracts
{
    public interface IChatService
    {
        Task<Chat> CreateChatAsync(List<long> userIds);
        Task AddParticipantAsync(int chatId, long userId);
        Task RemoveParticipantAsync(int chatId, long userId);
        Task<List<ResultChatDto>> GetChatsByUserIdAsync(long userId);
        Task<List<Message>> GetMessagesAsync(int chatId);
    }

}
