namespace Chat_Application_ServerAPI.Data.Models.DTO.ChatRoom
{
    public class AddChatRoomForm
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public bool IsGroup { get; set; }
        public string[] Users { get; set; }

    }
}
