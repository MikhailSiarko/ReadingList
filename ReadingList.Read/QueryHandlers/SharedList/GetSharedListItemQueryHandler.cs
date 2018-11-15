using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class GetSharedListItemQueryHandler : QueryHandler<GetSharedListItemQuery, SharedBookListItemDto>
    {
        public GetSharedListItemQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<SharedBookListItemDto> Handle(SqlQueryContext<GetSharedListItemQuery, SharedBookListItemDto> context)
        {
            using (var reader = await DbConnection.QueryMultipleAsync(context.Sql, context.Parameters))
            {
                var item = await reader.ReadSingleOrDefaultAsync<SharedBookListItemDto>() ??
                           throw new ObjectNotExistException<SharedBookListItem>(new OnExceptionObjectDescriptor
                           {
                               ["Item Id"] = context.Query.ItemId.ToString()
                           });

                var tags = (await reader.ReadAsync<(string TagName, int BookId)>()).ToList();

                item.Tags = tags.Where(t => t.BookId == item.BookId).Select(t => t.TagName);

                return item;
            }
        }
    }
}