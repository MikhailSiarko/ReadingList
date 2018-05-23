using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Authentication;
using ReadingList.ReadModel;
using UserRm = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.QueryHandlers
{   
    public class LoginUserQueryHandler : QueryHandler<LoginUserQuery, AuthenticationData>
    {
        private readonly ReadingListConnection _connection;
        private readonly IAuthenticationService _authenticationService;
        public LoginUserQueryHandler(IAuthenticationService authenticationService, ReadingListConnection connection)
        {
            _authenticationService = authenticationService;
            _connection = connection;
        }

        protected override async Task<AuthenticationData> Process(LoginUserQuery query)
        {  
            var user = await _connection.QuerySingle<UserRm>("SELECT Id, Login, Password FROM Users WHERE Login = @login",
                new { email =query.Login });
            var token = _authenticationService.EncodeSecurityToken(user);
            return _authenticationService.GenerateAuthResponse(token, user);
        }
    }
}