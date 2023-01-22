namespace Chat_Application_ServerAPI.Data.Models.DTO.message
{
    public class MessageForm
    {
        public string UserId { get; set; }
        public int ChatId { get; set; }
        public string MessageContent { get; set; }
    }
}
