using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel;
using UserRM = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.QueryHandlers
{
    public class GetUserQueryHandler : QueryHandler<GetUserQuery, UserRM>
    {
        private readonly ReadingListConnection _connection;

        public GetUserQueryHandler(ReadingListConnection connection)
        {
            _connection = connection;
        }

        protected override async Task<UserRM> Process(GetUserQuery query)
        {
            return await _connection.QuerySingle<UserRM>("SELECT Id, Login From Users WHERE Id = @id",
                new {id = query.UserId});
        }
    }
}