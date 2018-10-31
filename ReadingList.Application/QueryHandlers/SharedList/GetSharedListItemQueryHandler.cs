using System.Linq;
using System.Threading.Tasks;
using ReadingList.Domain.Entities;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Queries;
using ReadingList.Read;

namespace ReadingList.Application.QueryHandlers
{
    public class GetSharedListItemQueryHandler : QueryHandler<GetSharedListItemQuery, SharedBookListItemDto>
    {
        public GetSharedListItemQueryHandler(IApplicationDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<SharedBookListItemDto> Handle(GetSharedListItemQuery query)
        {
            return await DbConnection.QuerySingleAsync(query.SqlQueryContext,
                       async reader =>
                       {
                           var item = (await reader.ReadAsync<SharedBookListItemDto>()).SingleOrDefault();

                           if (item == null)
                               return null;

                           var tags = await reader.ReadAsync<string>();

                           item.Tags = tags;

                           return item;
                       }) ??
                   throw new ObjectNotExistException<SharedBookListItem>(new OnExceptionObjectDescriptor
                   {
                       ["Item Id"] = query.ItemId.ToString()
                   });
        }
    }
}