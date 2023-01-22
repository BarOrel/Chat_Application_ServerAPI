using Chat_Application_ServerAPI.Data.Models;
using System;
namespace Chat_Application_ServerAPI.Data.Repository.ChatRoomRepo
{
    public interface IChatRoomRepository
    {
        Task<ICollection<ChatRoom>> GetAll();
        Task<ChatRoom> GetById(int id);
        Task Add(ChatRoom entity);
        Task Update(ChatRoom entity, int id);
        Task DeleteById(int id);
    }
}

