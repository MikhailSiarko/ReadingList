using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingList.Application.DTO.BookList;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Queries;
using ReadingList.Read;
using ReadingList.Domain.Entities;

namespace ReadingList.Application.QueryHandlers
{
    public class GetPrivateListQueryHandler : QueryHandler<GetPrivateListQuery, PrivateBookListDto>
    {
        public GetPrivateListQueryHandler(IApplicationDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<PrivateBookListDto> Handle(GetPrivateListQuery query)
        {
            var listDictionary = new Dictionary<int, PrivateBookListDto>();

            var privateList =
                await DbConnection.QueryFirstAsync<PrivateBookListDto, PrivateBookListItemDto, PrivateBookListDto>(
                    query.SqlQueryContext, 
                    (list, item) =>
                    {
                        if (!listDictionary.TryGetValue(list.Id, out var listEntry))
                        {
                            listEntry = list;
                            listEntry.Items = new List<PrivateBookListItemDto>();
                            listDictionary.Add(listEntry.Id, list);
                        }

                        if (item != null)
                            ((List<PrivateBookListItemDto>)listEntry.Items).Add(item);
                        return listEntry;
                    }) ??
                throw new ObjectNotExistException<BookList>(new OnExceptionObjectDescriptor
                {
                    ["Email"] = query.Login
                });

            return privateList;
        }
    }
}