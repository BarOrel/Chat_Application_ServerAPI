using Chat_Application_ServerAPI.Data.Models;
using Chat_Application_ServerAPI.Data.Repository.ChatRoomRepo;
using Chat_Application_ServerAPI.Data.Repository.MessageRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace Chat_Application_ServerAPI.Data.Service
{
    public class Service : IService
    {
        readonly IChatRoomRepository chatRoomRepository;
        private readonly IMessageRepository messageRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public Service(IChatRoomRepository _chatRoomRepository, IMessageRepository _messageRepository, UserManager<ApplicationUser> _userManager)
        {
            chatRoomRepository = _chatRoomRepository;
            messageRepository = _messageRepository;
            userManager = _userManager;
        }
        public async Task<IEnumerable<Message>> GetMessagesByRoom(ChatRoom chat)
        {
            var result = await messageRepository.GetAll();
            return result.Where(x => x.ChatRoomId == chat.Id);
        }

        public async Task<IEnumerable<ChatRoom>> GetRoomsByUser(string user)
        {
            var result = await chatRoomRepository.GetAll();
            var vesult = result.Where(x => x.Users.Any(n => n.Id == user));
            return vesult;

        }

        public async Task<IEnumerable<Message>> GetAllMessages() => await messageRepository.GetAll();
        public async Task DeleteMessageById(int id) => await messageRepository.DeleteById(id);
        public async Task DeleteChatById(int id) => await chatRoomRepository.DeleteById(id);
        public async Task<ChatRoom> GetRoomById(int chat) => await chatRoomRepository.GetById(chat);
        public async Task CreateChatRoom(ChatRoom entity) => await chatRoomRepository.Add(entity);

        public async Task AddMessage(Message entity) => await messageRepository.Add(entity);



    }
}
