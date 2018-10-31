using System.Threading.Tasks;
using ReadingList.Application.DTO.User;
using ReadingList.Domain.Entities.Identity;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Queries;
using ReadingList.Application.Services.Authentication;
using ReadingList.Application.Services.Encryption;
using ReadingList.Read;

namespace ReadingList.Application.QueryHandlers
{
    public class LoginUserQueryHandler : QueryHandler<LoginUserQuery, AuthenticationData>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEncryptionService _encryptionService;

        public LoginUserQueryHandler(IAuthenticationService authenticationService,
            IEncryptionService encryptionService, IApplicationDbConnection connection) : base(connection)
        {
            _authenticationService = authenticationService;
            _encryptionService = encryptionService;
        }

        protected override async Task<AuthenticationData> Handle(LoginUserQuery query)
        {
            var user = await DbConnection.QuerySingleAsync<UserDto>(query.SqlQueryContext) ??
                       throw new ObjectNotExistException<User>(new OnExceptionObjectDescriptor
                       {
                           ["Email"] = query.Login
                       });
            
            if(_encryptionService.Encrypt(query.Password) != user.Password)
                throw new WrongPasswordException();
            
            return _authenticationService.Authenticate(user);
        }
    }
}