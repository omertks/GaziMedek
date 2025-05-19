using MessageService.Models;

namespace MessageService.Services.Abstracts
{
    public interface IMessageService
    {
        Task<Message> SendMessageAsync(int chatId, long senderId, string content);
    }


}
