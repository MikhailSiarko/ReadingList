using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Exceptions;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class GetPrivateListQueryHandler : QueryHandler<GetPrivateList, PrivateBookListDto>
    {
        private readonly Dictionary<int, PrivateBookListDto> _listRegistry;

        public GetPrivateListQueryHandler(IDbConnection dbConnection) : base(dbConnection)
        {
            _listRegistry = new Dictionary<int, PrivateBookListDto>();
        }

        protected override async Task<PrivateBookListDto> Handle(
            SqlQueryContext<GetPrivateList, PrivateBookListDto> context)
        {
            var list = (await DbConnection.QueryAsync<PrivateBookListDto, PrivateBookListItemDto, PrivateBookListDto>(
                context.Sql, CollectItems, context.Parameters)).FirstOrDefault();

            if (list == null)
            {
                throw new ObjectNotExistForException<BookList, User>(null, new OnExceptionObjectDescriptor
                {
                    ["Id"] = context.Query.UserId.ToString()
                });
            }

            return list;
        }

        private PrivateBookListDto CollectItems(PrivateBookListDto list, PrivateBookListItemDto item)
        {
            if (!_listRegistry.TryGetValue(list.Id, out var listEntry))
            {
                listEntry = list;
                listEntry.Items = new List<PrivateBookListItemDto>();
                _listRegistry.Add(listEntry.Id, list);
            }

            if (item != null)
                ((List<PrivateBookListItemDto>) listEntry.Items).Add(item);

            return listEntry;
        }
    }
}