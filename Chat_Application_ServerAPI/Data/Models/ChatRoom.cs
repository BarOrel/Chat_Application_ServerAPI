using System;
using System.ComponentModel.DataAnnotations.Schema;
using Chat_Application_ServerAPI.Data.Models.Enums;

namespace Chat_Application_ServerAPI.Data.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public RoomType Type { get; set; }
        public List<ApplicationUser> Users { get; set; }

    }
}

