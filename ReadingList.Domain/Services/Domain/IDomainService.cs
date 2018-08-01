using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Queries;

namespace ReadingList.Domain.Services
{
    public interface IDomainService
    {
        Task ExecuteAsync(ICommand command);

        Task<TResult> AskAsync<TResult>(IQuery<TResult> query);

        void Execute(ICommand command);

        TResult Ask<TResult>(IQuery<TResult> query);
    }
}