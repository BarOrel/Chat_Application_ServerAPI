using System;

namespace Chat_Application_ServerAPI.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int ChatRoomId { get; set; }
        public string UserId { get; set; }
        public DateTime SentTime { get; set; }

    }
}

