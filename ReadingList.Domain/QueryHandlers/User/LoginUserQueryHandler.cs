using System.Threading.Tasks;
using Cinch.SqlBuilder;
using ReadingList.Domain.Abstractions;
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
        public LoginUserQueryHandler(IAuthenticationService authenticationService,
            IEncryptionService encryptionService, IReadDbConnection dbConnection)
        {
            _authenticationService = authenticationService;
            _encryptionService = encryptionService;
            _dbConnection = dbConnection;
        }

        protected override async Task<AuthenticationData> Handle(LoginUserQuery query)
        {
            var sql = new SqlBuilder()
                .Select("Id", "Login", "ProfileId", "(SELECT Name FROM Roles WHERE Id = RoleId) AS Role")
                .From("Users")
                .Where("Login = @login")
                .Where("Password = @password")
                .ToSql();
            var user = await _dbConnection.QuerySingleAsync<UserRm>(sql,
                new {login = query.Login, password = _encryptionService.Encrypt(query.Password)});
            return _authenticationService.Authenticate(user, query);
        }
    }
}