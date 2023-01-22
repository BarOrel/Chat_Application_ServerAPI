using System;
using Chat_Application_ServerAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application_ServerAPI.Data.Repository.MessageRepo
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext dbContext;

        public MessageRepository(ChatDbContext _dbContext) => dbContext = _dbContext;

        public async Task Add(Message entity)
        {
            await dbContext.Messages.AddAsync(entity);
            dbContext.SaveChanges();
        }

        public async Task<IEnumerable<Message>> GetAll() => await dbContext.Set<Message>().ToListAsync();

        public async Task<Message> GetById(int id) => await dbContext.Set<Message>().FirstOrDefaultAsync(n => n.Id == id);

        public async Task DeleteById(int id)
        {
            var result = await dbContext.Messages.FirstOrDefaultAsync(n => n.Id == id);
            dbContext.Remove(result);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(Message entity, int id)
        {

        }
    }
}

