using System.Threading.Tasks;
using ReadingList.Domain.Entities;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Queries;
using ReadingList.Read;

namespace ReadingList.Application.QueryHandlers
{
    public class GetPrivateListItemQueryHandler : QueryHandler<GetPrivateListItemQuery, PrivateBookListItemDto>
    {
        public GetPrivateListItemQueryHandler(IApplicationDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<PrivateBookListItemDto> Handle(GetPrivateListItemQuery query)
        {
            return await DbConnection.QuerySingleAsync<PrivateBookListItemDto>(query.SqlQueryContext) ??
                   throw new ObjectNotExistException<BookList>(new OnExceptionObjectDescriptor
                   {
                       ["Id"] = query.ItemId.ToString()
                   });
        }
    }
}