using System.Threading.Tasks;
using Cinch.SqlBuilder;
using ReadingList.Domain.Abstractions;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel.Models;
using PrivateListItemRm = ReadingList.ReadModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListItemQueryHandler : QueryHandler<GetPrivateListItemQuery, PrivateListItemRm>
    {
        private readonly IReadDbConnection _dbConnection;

        public GetPrivateListItemQueryHandler(IReadDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        protected override async Task<PrivateListItemRm> Handle(GetPrivateListItemQuery query)
        {
            var sql = new SqlBuilder()
                .Select("Id", "Title", "Author", "Status", "ReadingTimeInTicks")
                .From("PrivateBookListItems")
                .Where(
                    "BookListId = (SELECT Id From BookLists WHERE OwnerId = (SELECT Id FROM Users WHERE Login = @login) AND Type = @type) AND Title = @title AND Author = @author")
                .ToSql();
            return await _dbConnection.QuerySingleAsync<PrivateListItemRm>(sql, new
            {
                login = query.Login,
                type = BookListType.Private,
                title = query.Title,
                author = query.Author
            });
        }
    }
}