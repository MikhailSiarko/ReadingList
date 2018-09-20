using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingList.Domain.Infrastructure;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.QueryHandlers
{
    public class BookStatusesQueryHandler : QueryHandler<BookStatusesQuery, IEnumerable<NameValuePair>>
    {
        private readonly IReadDbConnection _readDbConnection;

        public BookStatusesQueryHandler(IReadDbConnection readDbConnection)
        {
            _readDbConnection = readDbConnection;
        }

        protected override async Task<IEnumerable<NameValuePair>> Handle(BookStatusesQuery query)
        {
            return await Task.Run(() => BookItemStatus.Read.ToNameValuePairs());
        }
    }
}