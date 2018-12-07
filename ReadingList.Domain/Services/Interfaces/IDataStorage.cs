using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingList.Models;

namespace ReadingList.Domain.Services.Interfaces
{
    public interface IDataStorage : IDisposable
    {
        Task<T> GetAsync<T>(int id) where T : Entity;

        Task SaveAsync<T>(T entity) where T : Entity;

        Task DeleteAsync<T>(int id) where T : Entity;

        Task SaveBatchAsync<T>(IEnumerable<T> entities) where T : Entity;
    }
}