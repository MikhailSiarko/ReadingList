using System.Collections.Generic;
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
    public class GetSharedListQueryHandler : QueryHandler<GetSharedListQuery, SharedBookListDto>
    {
        public GetSharedListQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<SharedBookListDto> Handle(SqlQueryContext<GetSharedListQuery, SharedBookListDto> context)
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
                    (await reader.ReadAsync<(string TagName, int BookId)>()).ToLookup(tuple => tuple.BookId);

                foreach (var item in items)
                {
                    item.Tags = itemsTags[item.BookId].Select(x => x.TagName).ToList();
                }

                list.Items = items;

                list.CanEdit = (await reader.ReadSingleAsync<Access>()).CanEdit;

                return list;
            }
        }
        
        private class Access
        {
            public bool CanEdit { get; set; }
        }
    }
}