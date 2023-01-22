using Chat_Application_ServerAPI.Data;
using Chat_Application_ServerAPI.Data.Models;
using Chat_Application_ServerAPI.Data.Models.DTO.chatRoom;
using Chat_Application_ServerAPI.Data.Models.DTO.ChatRoom;

using Chat_Application_ServerAPI.Data.Repository.ChatRoomRepo;
using Chat_Application_ServerAPI.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application_ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly IService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IChatRoomRepository chatDb;


        public ChatRoomController(IService service,UserManager<ApplicationUser> userManager,IChatRoomRepository chatDb)
        {
            this.service = service;
            this.userManager = userManager;
            this.chatDb = chatDb;
        }

        [HttpGet("{userid}")]
        public async Task<IActionResult> GetByUser(string UserId)
        {
            List<chatRoomView> roomView = new();
            var res = await service.GetRoomsByUser(UserId);
            foreach (var item in res)
            {
                chatRoomView chat = new();
                chat.Chat = item;
                   
                var result = await service.GetMessagesByRoom(item);
                chat.LastMessage = result.LastOrDefault();
                if (chat.LastMessage == null)
                {
                     Message message = new() { Content = "Chat is empty , Say Hey ! :)" };
                     chat.LastMessage = message;
                     chat.SentTime = "";
                }
                chat.SentTime = chat.LastMessage.SentTime.ToShortTimeString();
                roomView.Add(chat);
            }

            return Ok(roomView);
        }

        [HttpGet("GetChat/{ChatRoomId}")]
        public async Task<IActionResult> GetByUser(int ChatRoomId)
        {
            var chat = await service.GetRoomById(ChatRoomId);
            var messages = await service.GetMessagesByRoom(chat);
            ChatView chatView = new()
            {
                chat = chat,
                messages = messages,
            };

            return Ok(chatView);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddChatRoomForm entity)
        {
            var user = await userManager.FindByIdAsync(entity.UserId);
            ChatRoom chatRoom = new()
            {
                Title = entity.Title,
                Description = entity.Description,
                ImgUrl = entity.ImgUrl,
                Users = new()
            };

            foreach (var item in entity.Users)
            {
                var userFromDB = await userManager.FindByNameAsync(item);
                chatRoom.Users.Add(userFromDB);
            }
            chatRoom.Users.Add(user); 
            await service.CreateChatRoom(chatRoom);
            return Ok(chatRoom);
        }

        [HttpDelete("Delete/{ChatRoomId}")]
        public async Task<IActionResult> Delete(int ChatRoomId)
        {
            var room = await service.GetRoomById(ChatRoomId);
            var res = await service.GetMessagesByRoom(room);
            foreach (var item in res)
            {
                await service.DeleteMessageById(item.Id);
            }
            await service.DeleteChatById(ChatRoomId);
            return Ok(ChatRoomId);
        }





    }
}
