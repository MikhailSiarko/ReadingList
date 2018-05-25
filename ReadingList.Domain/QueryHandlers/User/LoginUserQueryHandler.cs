using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Authentication;
using ReadingList.Domain.Services.Encryption;
using ReadingList.ReadModel;
using UserRm = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.QueryHandlers
{   
    public class LoginUserQueryHandler : QueryHandler<LoginUserQuery, AuthenticationData>
    {
        private readonly ReadDbConnection _dbConnection;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEncryptionService _encryptionService;
        public LoginUserQueryHandler(IAuthenticationService authenticationService, ReadDbConnection dbConnection,
            IEncryptionService encryptionService)
        {
            _authenticationService = authenticationService;
            _dbConnection = dbConnection;
            _encryptionService = encryptionService;
        }

        protected override async Task<AuthenticationData> Process(LoginUserQuery query)
        {
            var user = await _dbConnection.QuerySingle<UserRm>(
                @"SELECT u.Id, u.Login, u.ProfileId, (SELECT Name FROM Roles WHERE Id = u.RoleId) AS Role 
                  FROM Users AS u WHERE Login = @login AND Password = @password",
                new {login = query.Login, password = _encryptionService.Encrypt(query.Password)});
            return _authenticationService.Authenticate(user, query);
        }
    }
}