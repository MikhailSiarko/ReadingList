using System.Threading.Tasks;
using Dapper;
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

        protected override async Task<PrivateListItemRm> Process(GetPrivateListItemQuery query)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder
                .Select("Id, Title, Author, Status, ReadingTimeInTicks")
                .Where(
                    "BookListId = (SELECT Id From BookLists WHERE OwnerId = (SELECT Id FROM Users WHERE Login = @login) AND Type = @type) AND Title = @title AND Author = @author")
                .AddParameters(new
                {
                    login = query.Login,
                    type = BookListType.Private,
                    title = query.Title,
                    author = query.Author
                });
            var getItemQuery = sqlBuilder.AddTemplate("SELECT /**select**/ FROM PrivateBookListItems /**where**/");
            return await _dbConnection.QuerySingleAsync<PrivateListItemRm>(getItemQuery.RawSql,
                getItemQuery.Parameters);
        }
    }
}