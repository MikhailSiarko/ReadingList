using System.Threading.Tasks;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Authentication;
using ReadingList.Domain.Services.Encryption;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using UserRm = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.QueryHandlers
{   
    public class LoginUserQueryHandler : QueryHandler<LoginUserQuery, AuthenticationData>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEncryptionService _encryptionService;
        private readonly IUserSqlService _userSqlService;

        public LoginUserQueryHandler(IAuthenticationService authenticationService,
            IEncryptionService encryptionService, IReadDbConnection dbConnection, IUserSqlService userSqlService)
        {
            _authenticationService = authenticationService;
            _encryptionService = encryptionService;
            _dbConnection = dbConnection;
            _userSqlService = userSqlService;
        }

        protected override async Task<AuthenticationData> Handle(LoginUserQuery query)
        {
            var user = await _dbConnection.QueryFirstAsync<UserRm>(_userSqlService.GetUserByLoginSqlQuery(),
                           new {login = query.Login}) ??
                       throw new ObjectNotExistException<UserRm>(new {email = query.Login});
            
            if(_encryptionService.Encrypt(query.Password) != user.Password)
                throw new WrongPasswordException();
            
            return _authenticationService.Authenticate(user);
        }
    }
}