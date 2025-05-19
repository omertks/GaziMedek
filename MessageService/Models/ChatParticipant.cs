namespace MessageService.Models
{
    public class ChatParticipant
    {
        public long ChatId { get; set; }
        public Chat Chat { get; set; }

        public long UserId { get; set; }
    }

}
