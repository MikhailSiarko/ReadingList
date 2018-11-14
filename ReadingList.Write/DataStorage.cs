using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Models;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Write.Infrastructure;

namespace ReadingList.Write
{
    public class DataStorage : IDataStorage
    {
        private readonly WriteDbContext _dbContext;

        public DataStorage(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public async Task<T> GetAsync<T>(int id) where T : Entity
        {
            return await _dbContext.Set<T>().Include(_dbContext.GetIncludePaths<T>())
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync<T>(T entity) where T : Entity
        {
            if (entity.Id == default(int))
            {
                await _dbContext.Set<T>().AddAsync(entity);
            }
            else
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(int id) where T : Entity
        {
            var entity = await _dbContext.Set<T>().Include(_dbContext.GetIncludePaths<T>())
                .SingleOrDefaultAsync(x => x.Id == id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}