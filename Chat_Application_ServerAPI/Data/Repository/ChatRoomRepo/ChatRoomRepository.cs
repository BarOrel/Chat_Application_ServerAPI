using System;
using System.Linq;
using Chat_Application_ServerAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace Chat_Application_ServerAPI.Data.Repository.ChatRoomRepo
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly ChatDbContext dbContext;

        public ChatRoomRepository(ChatDbContext _dbContext) => dbContext = _dbContext;

        public async Task Add(ChatRoom entity)
        {
            await dbContext.ChatRooms.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<ChatRoom>> GetAll()
        {
            
            return await dbContext.ChatRooms.Include(n => n.Users).ToListAsync();
            
        }

        public async Task<ChatRoom> GetById(int id) 
        { 
           var res = await dbContext.ChatRooms.Include(n => n.Users).ToListAsync();
            return res.FirstOrDefault(n => n.Id == id);
        }

        public async Task DeleteById(int id)
        {
            var result = await dbContext.ChatRooms.FirstOrDefaultAsync(n => n.Id == id);
            dbContext.ChatRooms.Remove(result);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(ChatRoom entity, int id)
        {

        }
    }
}

