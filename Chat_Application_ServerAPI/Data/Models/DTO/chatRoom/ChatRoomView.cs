namespace Chat_Application_ServerAPI.Data.Models.DTO.chatRoom
{
    public class chatRoomView
    {
        public Models.ChatRoom Chat { get; set; }
        public Message LastMessage { get; set; }
        public string SentTime { get; set; }
    }
}
