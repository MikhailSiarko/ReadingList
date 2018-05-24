using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Authentication;
using ReadingList.ReadModel;
using UserRm = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.QueryHandlers
{   
    public class LoginUserQueryHandler : QueryHandler<LoginUserQuery, AuthenticationResult>
    {
        private readonly ReadingListConnection _connection;
        private readonly IAuthenticationService _authenticationService;
        public LoginUserQueryHandler(IAuthenticationService authenticationService, ReadingListConnection connection)
        {
            _authenticationService = authenticationService;
            _connection = connection;
        }

        protected override async Task<AuthenticationResult> Process(LoginUserQuery query)
        {  
            var user = await _connection.QuerySingle<UserRm>("SELECT Id, Login, ProfileId, RoleId FROM Users WHERE Login = @login AND @password",
                new { login = query.Login, password = query.Password });
            return _authenticationService.Authenticate(user, query);
        }
    }
}