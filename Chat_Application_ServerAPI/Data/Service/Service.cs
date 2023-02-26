using Chat_Application_ServerAPI.Data.Models;
using Chat_Application_ServerAPI.Data.Models.DTO.chatRoom;
using Chat_Application_ServerAPI.Data.Repository;
using Chat_Application_ServerAPI.Data.Repository.ChatRoomRepo;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
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
        private readonly IGenericRepository<Message> messageRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public Service(IChatRoomRepository _chatRoomRepository, IGenericRepository<Message> _messageRepository, UserManager<ApplicationUser> _userManager)
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
        public async Task DeleteMessageById(Message entity) => await messageRepository.Delete(entity);
        public async Task DeleteChatById(int id)
        {
            var room = await chatRoomRepository.GetById(id);
            var res = await GetMessagesByRoom(room);
            foreach (var item in res)
            {
                await messageRepository.Delete(item);
            }
            await chatRoomRepository.DeleteById(id);
        }
        public async Task<ChatRoom> GetRoomById(int chat) => await chatRoomRepository.GetById(chat);
        public async Task CreateChatRoom(ChatRoom entity) => await chatRoomRepository.Add(entity);

        public async Task AddMessage(Message entity) => await messageRepository.Insert(entity);

        public async Task<List<chatRoomView>> GetChatView(string UserId)
        {
            List<chatRoomView> roomView = new();
            var res = await GetRoomsByUser(UserId);
            foreach (var item in res)
            {
                chatRoomView chat = new();
                chat.Chat = item;

                var result = await GetMessagesByRoom(item);
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
            roomView = roomView.OrderByDescending(n => n.SentTime).ToList();
            return roomView;
        }

    }
}
