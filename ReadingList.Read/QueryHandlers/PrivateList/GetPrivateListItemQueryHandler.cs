using System.Data;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Exceptions;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class GetPrivateListItemQueryHandler : QueryHandler<GetPrivateListItem, PrivateBookListItemDto>
    {
        public GetPrivateListItemQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<PrivateBookListItemDto> Handle(
            SqlQueryContext<GetPrivateListItem, PrivateBookListItemDto> context)
        {
            return await DbConnection.QuerySingleOrDefaultAsync<PrivateBookListItemDto>(context.Sql,
                       context.Parameters) ??
                   throw new ObjectNotExistException<BookList>(new OnExceptionObjectDescriptor
                   {
                       ["Id"] = context.Query.ItemId.ToString()
                   });
        }
    }
}