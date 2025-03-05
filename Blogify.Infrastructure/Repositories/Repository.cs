using Blogify.Infrastructure.Data;
using Blogify.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogify.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BlogifyDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(BlogifyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            var existingEntity = await _dbSet.FindAsync(GetPrimaryKeyValue(entity));

            if (existingEntity == null)
                throw new Exception($"{typeof(T)} not found");

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }


        private object GetPrimaryKeyValue(T entity)
        {
            var keyName = _context.Model.FindEntityType(typeof(T))
                .FindPrimaryKey()
                .Properties
                .Select(x => x.Name)
                .FirstOrDefault();

            return entity.GetType().GetProperty(keyName)?.GetValue(entity, null);
        }
    }
}
