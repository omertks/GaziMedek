using System.Text.Json.Serialization;

namespace MessageService.Models
{
    public class Chat
    {
        public long Id { get; set; }


        [JsonIgnore] // JSON dönüşünde bu listeyi görmezden gel
        public List<ChatParticipant> Participants { get; set; } = new();
        public List<Message> Messages { get; set; } = new();
    }


}
