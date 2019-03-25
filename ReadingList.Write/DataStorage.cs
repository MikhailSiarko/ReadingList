using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models;

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
            return await _dbContext.Table<T>().SingleOrDefaultAsync(x => x.Id == id);
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
            var entity = await _dbContext.Table<T>().SingleOrDefaultAsync(x => x.Id == id);

            _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveBatchAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            var enumerable = entities.ToList();
            if (enumerable.Any())
            {

                var existedEntities = enumerable.Where(e => e.Id != default(int)).ToList();

                if (existedEntities.Any())
                {
                    _dbContext.RemoveRange(existedEntities);

                    await _dbContext.SaveChangesAsync();
                }

                await _dbContext.AddRangeAsync(enumerable);

                await _dbContext.SaveChangesAsync();
            }

        }
    }
}