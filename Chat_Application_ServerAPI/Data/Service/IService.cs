using Chat_Application_ServerAPI.Data.Models;

namespace Chat_Application_ServerAPI.Data.Service
{
    public interface IService
    {
        Task<IEnumerable<ChatRoom>> GetRoomsByUser(string user);
        Task<IEnumerable<Message>> GetMessagesByRoom(ChatRoom Chat);
        Task<IEnumerable<Message>> GetAllMessages();
        Task DeleteChatById(int id);
        Task DeleteMessageById(int id);
        Task<ChatRoom> GetRoomById(int id);
        Task CreateChatRoom(ChatRoom entity);
        Task AddMessage(Message entity);
    }
}
