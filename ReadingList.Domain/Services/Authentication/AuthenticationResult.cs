using ReadingList.Domain.Absrtactions;
using UserRM = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.Services.Authentication
{
    public class AuthenticationResult : Result<AuthenticationData>
    {
        
        public static AuthenticationResult Succeed(AuthenticationData data) => new AuthenticationResult(true, data);
        public static AuthenticationResult Failed(string errorMessage) => new AuthenticationResult(false, errorMessage);
        
        private AuthenticationResult(bool isSucceed, AuthenticationData data) : base(isSucceed)
        {
            Data = data;
        }
        
        private AuthenticationResult(bool isSucceed, string errorMessage) : base(isSucceed, errorMessage)
        {
        }
    }
}