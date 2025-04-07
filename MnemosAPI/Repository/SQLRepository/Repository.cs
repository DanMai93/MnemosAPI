
using Microsoft.EntityFrameworkCore;
using MnemosAPI.Data;

namespace MnemosAPI.Repository.SQLRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MnemosDbContext dbContext;

        public Repository(MnemosDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public virtual async Task<T> AddAsync(T item)
        {
            await dbContext.Set<T>().AddAsync(item);
            await dbContext.SaveChangesAsync();
            return item;
        }

        public virtual async Task DeleteByIdAsync(int id)
        {
            var existingItem = await dbContext.Set<T>().FindAsync(id);

            if (existingItem == null)
            {
                throw new InvalidOperationException("Id non trovato. Operazione non valida.");
            }

            dbContext.Set<T>().Remove(existingItem);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> UpdateAsync(T item)
        {
            dbContext.Set<T>().Update(item);

            await dbContext.SaveChangesAsync();
            return item;
        }
    }
}
