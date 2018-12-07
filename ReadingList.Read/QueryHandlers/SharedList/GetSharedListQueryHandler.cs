using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Exceptions;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class GetSharedListQueryHandler : QueryHandler<GetSharedList, SharedBookListDto>
    {
        public GetSharedListQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<SharedBookListDto> Handle(
            SqlQueryContext<GetSharedList, SharedBookListDto> context)
        {
            using (var reader = await DbConnection.QueryMultipleAsync(context.Sql, context.Parameters))
            {
                var list = (await reader.ReadAsync<SharedBookListDto>()).SingleOrDefault() ??
                           throw new ObjectNotExistException<BookList>(new OnExceptionObjectDescriptor
                           {
                               ["Id"] = context.Query.ListId.ToString()
                           });

                var tags = (await reader.ReadAsync<string>()).ToList();

                list.Tags = tags;

                var items = new List<SharedBookListItemDto>(await reader.ReadAsync<SharedBookListItemDto>());

                var itemsTags =
                    (await reader.ReadAsync<(string TagName, int BookId)>()).ToLookup(t => t.BookId);

                foreach (var item in items)
                {
                    item.Tags = itemsTags[item.BookId].Select(x => x.TagName).ToList();
                }

                list.Items = items;

                return list;
            }
        }
    }
}