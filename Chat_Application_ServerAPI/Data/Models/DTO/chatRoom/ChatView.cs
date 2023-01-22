namespace Chat_Application_ServerAPI.Data.Models.DTO.chatRoom
{
    public class ChatView
    {
        public Models.ChatRoom chat { get; set; }
        public IEnumerable<Message> messages { get; set; }
    }
}
