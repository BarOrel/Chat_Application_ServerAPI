using Chat_Application_ServerAPI.Data.Models;
using Chat_Application_ServerAPI.Data.Models.DTO.chatRoom;

namespace Chat_Application_ServerAPI.Data.Service
{
    public interface IService
    {
        Task<IEnumerable<ChatRoom>> GetRoomsByUser(string user);
        Task<IEnumerable<Message>> GetMessagesByRoom(ChatRoom Chat);
        Task<IEnumerable<Message>> GetAllMessages();
        Task DeleteChatById(int id);
        Task DeleteMessageById(Message entity);
        Task<List<chatRoomView>> GetChatView(string UserId);
        Task<ChatRoom> GetRoomById(int id);
        Task CreateChatRoom(ChatRoom entity);
        Task AddMessage(Message entity);
    }
}
