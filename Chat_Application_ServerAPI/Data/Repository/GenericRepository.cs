using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Application_ServerAPI.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ChatDbContext context;

        public GenericRepository(ChatDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<T>> GetAll() => await context.Set<T>().ToListAsync();

        public async Task<T> GetById(object id)
        {
            var result = await context.Set<T>().FindAsync(id);
            return result;

        }

        public async Task Insert(T obj)
        {
            await context.Set<T>().AddAsync(obj);
            await Save();
        }

        public async Task<T> Find(T obj)
        {
            var res = await context.Set<T>().ToListAsync();
            return res.FirstOrDefault(n => n == obj);
        }

        public async Task Update(T obj)
        {
            context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public async Task Delete(T entity)
        {
            T existing = await Find(entity);
            context.Remove(existing);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }




    }
}
