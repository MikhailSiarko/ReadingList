using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Authentication;
using ReadingList.Domain.Services.Encryption;
using ReadingList.ReadModel.DbConnection;
using UserRm = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.QueryHandlers
{   
    public class LoginUserQueryHandler : QueryHandler<LoginUserQuery, AuthenticationData>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEncryptionService _encryptionService;
        public LoginUserQueryHandler(IAuthenticationService authenticationService, IReadDbConnection dbConnection,
            IEncryptionService encryptionService)
        {
            _authenticationService = authenticationService;
            _dbConnection = dbConnection;
            _encryptionService = encryptionService;
        }

        protected override async Task<AuthenticationData> Process(LoginUserQuery query)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Select(
                    "Id, Login, ProfileId, (SELECT Name FROM Roles WHERE Id = RoleId) AS Role")
                .Where("Login = @login AND Password = @password")
                .AddParameters(new {login = query.Login, password = _encryptionService.Encrypt(query.Password)});
            var loginQuery = sqlBuilder.AddTemplate("SELECT /**select**/ FROM Users /**where**/");
            var user = await _dbConnection.QuerySingleAsync<UserRm>(loginQuery.RawSql, loginQuery.Parameters);
            return _authenticationService.Authenticate(user, query);
        }
    }
}