using Microsoft.IdentityModel.Tokens;

namespace Api.Authentication.AuthenticationOptions
{
    public interface IJwtOptions
    {       
        string Issuer { get; }
        string Audience { get; }
        int Lifetime { get; }
        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}