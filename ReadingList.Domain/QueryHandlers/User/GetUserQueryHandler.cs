using System.Threading.Tasks;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.QueryHandlers
{
    public class GetUserQueryHandler : QueryHandler<GetUserQuery, UserRm>
    {
        private readonly IDbReader _dbConnection;
        private readonly IUserSqlService _userSqlService;

        public GetUserQueryHandler(IDbReader dbConnection, IUserSqlService userSqlService)
        {
            _dbConnection = dbConnection;
            _userSqlService = userSqlService;
        }

        protected override async Task<UserRm> Handle(GetUserQuery query)
        {
            var user = await _dbConnection.QueryFirstAsync<UserRm>(_userSqlService.GetUserByIdSqlQuery(),
                           new {id = query.UserId}) ??
                       throw new ObjectNotExistException<UserRm>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = query.UserId.ToString()
                       });
            
            return user;
        }
    }
}