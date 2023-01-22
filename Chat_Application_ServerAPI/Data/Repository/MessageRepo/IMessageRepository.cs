using Chat_Application_ServerAPI.Data.Models;
using System;
namespace Chat_Application_ServerAPI.Data.Repository.MessageRepo
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAll();
        Task<Message> GetById(int id);
        Task Add(Message entity);
        Task Update(Message entity, int id);
        Task DeleteById(int id);
    }
}

