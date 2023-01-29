using Chat_Application_ServerAPI.Data.Models.DTO.ChatRoom;
using Chat_Application_ServerAPI.Data.Models;
using Chat_Application_ServerAPI.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Chat_Application_ServerAPI.Data.Models.DTO.message;

namespace Chat_Application_ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IService service;

        public MessageController(IService service)
        {
            this.service = service;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(MessageForm entity)
        {
            Message message = new()
            {
                ChatRoomId = entity.ChatId,
                UserId = entity.UserId,
                Content = entity.MessageContent,
                SentTime = DateTime.Now,

            };
            await service.AddMessage(message);
            return Ok(message);
        }

  




    }
}
