using System.Data;
using System.Threading.Tasks;
using Dapper;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Models.Write.Identity;
using ReadingList.Read.Queries;

namespace ReadingList.Read.QueryHandlers
{
    public class LoginUserQueryHandler : QueryHandler<LoginUser, AuthenticationDataDto>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEncryptionService _encryptionService;

        public LoginUserQueryHandler(IAuthenticationService authenticationService,
            IEncryptionService encryptionService, IDbConnection dbConnection) : base(dbConnection)
        {
            _authenticationService = authenticationService;
            _encryptionService = encryptionService;
        }

        protected override async Task<AuthenticationDataDto> Handle(
            SqlQueryContext<LoginUser, AuthenticationDataDto> context)
        {
            var user = await DbConnection.QuerySingleOrDefaultAsync<UserDto>(context.Sql, context.Parameters) ??
                       throw new ObjectNotExistException<User>(new OnExceptionObjectDescriptor
                       {
                           ["Login"] = context.Query.Login
                       });

            if (_encryptionService.Encrypt(context.Query.Password) != user.Password)
                throw new WrongPasswordException();

            return _authenticationService.Authenticate(user);
        }
    }
}