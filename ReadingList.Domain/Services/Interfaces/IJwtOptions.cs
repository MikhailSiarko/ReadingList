using Microsoft.IdentityModel.Tokens;

namespace ReadingList.Domain.Services.Interfaces
{
    public interface IJwtOptions
    {       
        string Issuer { get; }

        string Audience { get; }

        int Lifetime { get; }

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}