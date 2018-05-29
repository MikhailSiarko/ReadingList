using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Authentication;
using ReadingList.Domain.Services.Encryption;
using ReadingList.ReadModel.DbConnection;
using ReadingList.ReadModel.FluentSqlBuilder;
using UserRm = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.QueryHandlers
{   
    public class LoginUserQueryHandler : QueryHandler<LoginUserQuery, AuthenticationData>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEncryptionService _encryptionService;
        public LoginUserQueryHandler(IAuthenticationService authenticationService,
            IEncryptionService encryptionService, IReadDbConnection dbConnection)
        {
            _authenticationService = authenticationService;
            _encryptionService = encryptionService;
            _dbConnection = dbConnection;
        }

        protected override async Task<AuthenticationData> Handle(LoginUserQuery query)
        {
            var sqlResult = FluentSqlBuilder.NewBuilder()
                .Select("Id, Login, ProfileId, (SELECT Name FROM Roles WHERE Id = RoleId) AS Role")
                .From("Users")
                .Where("Login = @login AND Password = @password")
                .AddParameters(new {login = query.Login, password = _encryptionService.Encrypt(query.Password)})
                .Build();

            var user = await _dbConnection.QuerySingleAsync<UserRm>(sqlResult.RawSql, sqlResult.Parameters);
            return _authenticationService.Authenticate(user, query);
        }
    }
}