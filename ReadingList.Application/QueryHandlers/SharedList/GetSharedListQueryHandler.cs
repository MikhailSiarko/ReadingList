using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingList.Domain.Entities;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Queries;
using ReadingList.Read;

namespace ReadingList.Application.QueryHandlers
{
    public class GetSharedListQueryHandler : QueryHandler<GetSharedListQuery, SharedBookListDto>
    {
        public GetSharedListQueryHandler(IApplicationDbConnection dbConnection) : base(dbConnection)
        {
        }
        
        protected override async Task<SharedBookListDto> Handle(GetSharedListQuery query)
        {
            return await DbConnection.QuerySingleAsync(query.SqlQueryContext,
                    async reader =>
                    {
                        var list = (await reader.ReadAsync<SharedBookListDto>()).SingleOrDefault();

                        if (list == null) 
                            return null;
                        
                        var tags = (await reader.ReadAsync<string>()).ToList();

                        list.Tags = tags;

                        var items = new List<SharedBookListItemDto>(await reader.ReadAsync<SharedBookListItemDto>());

                        var itemsTags = (await reader.ReadAsync<(string TagName, int? ItemId)>()).ToList();

                        foreach (var itemRm in items)
                        {
                            itemRm.Tags =
                                itemsTags.Where(t => t.ItemId.HasValue && t.ItemId.Value == itemRm.Id)
                                    .Select(x => x.TagName).ToList();
                        }

                        list.Items = items;

                        return list;
                    }) ??
                throw new ObjectNotExistException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Id"] = query.ListId.ToString()
                });
        }
    }
}