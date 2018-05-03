using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions
{
    internal interface IJwtOptions
    {       
        string Issuer { get; set; }
        string Audience { get; set; }
        string Key { get; set; }
        int Lifetime { get; set; }
        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}