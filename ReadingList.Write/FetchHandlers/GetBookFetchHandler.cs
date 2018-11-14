using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Write.FetchHandlers
{
    public class GetBookFetchHandler : IFetchHandler<GetBookByAuthorAndTitleQuery, Book>
    {
        private readonly WriteDbContext _dbContext;

        public GetBookFetchHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> Fetch(GetBookByAuthorAndTitleQuery query)
        {
            return await _dbContext.Books.SingleOrDefaultAsync(b => b.Author == query.Author && b.Title == query.Title);
        }
    }
}