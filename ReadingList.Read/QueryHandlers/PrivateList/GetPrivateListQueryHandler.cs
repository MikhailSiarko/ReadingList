using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DAO.Identity;
using ReadingList.Domain.Models.DTO.BookLists;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class GetPrivateListQueryHandler : QueryHandler<GetPrivateListQuery, PrivateBookListDto>
    {
        public GetPrivateListQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        protected override async Task<PrivateBookListDto> Handle(SqlQueryContext<GetPrivateListQuery, PrivateBookListDto> context)
        {
            var listDictionary = new Dictionary<int, PrivateBookListDto>();
            return 
                (await DbConnection.QueryAsync<PrivateBookListDto, PrivateBookListItemDto, PrivateBookListDto>(
                    context.Sql, 
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
                    }, context.Parameters)).FirstOrDefault() ??
                throw new ObjectNotExistForException<BookList, User>(null, new OnExceptionObjectDescriptor
                {
                    ["Id"] = context.Query.UserId.ToString()
                });
        }
    }
}