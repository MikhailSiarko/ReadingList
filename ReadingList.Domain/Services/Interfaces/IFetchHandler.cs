using System.Threading.Tasks;

namespace ReadingList.Domain.Services.Interfaces
{
    public interface IFetchHandler<in TQuery, TEntity>
    {
        Task<TEntity> Fetch(TQuery query);
    }
}