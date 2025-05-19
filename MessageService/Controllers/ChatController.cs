using MessageService.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] List<long> userIds)
        {
            try
            {
                var chat = await _chatService.CreateChatAsync(userIds);
                return Ok(chat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{chatId}/participants")]
        public async Task<IActionResult> AddParticipant(int chatId, [FromQuery] long userId)
        {
            try
            {
                await _chatService.AddParticipantAsync(chatId, userId);
                return Ok("Participant added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{chatId}/participants")]
        public async Task<IActionResult> RemoveParticipant(int chatId, [FromQuery] long userId)
        {
            try
            {
                await _chatService.RemoveParticipantAsync(chatId, userId);
                return Ok("Participant removed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetUserChats(long userId)
        {
            var chats = await _chatService.GetChatsByUserIdAsync(userId);
            return Ok(chats);
        }

        [HttpGet("{chatId}/messages")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var messages = await _chatService.GetMessagesAsync(chatId);
            return Ok(messages);
        }
    }

}
