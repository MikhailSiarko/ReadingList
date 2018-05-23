using System.Linq;
using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Authentication;

namespace ReadingList.Domain.QueryHandlers
{   
    public class LoginUserQueryHandler : QueryHandler<LoginUserQuery, AuthenticationData>
    {
        private readonly IAuthenticationService _authenticationService;
        public LoginUserQueryHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        protected override Task<AuthenticationData> Process(LoginUserQuery query)
        {
            return Task.Run(() =>
            {
                var user = UserSource.GetSource()
                    .Single(u => u.Email == query.Email && u.Password == query.Password);
                var token = _authenticationService.EncodeSecurityToken(user);
                return _authenticationService.GenerateAuthResponse(token, user);
            });
        }
    }
}