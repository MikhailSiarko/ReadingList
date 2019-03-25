using System.Text;
using Microsoft.IdentityModel.Tokens;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.Services.Authentication
{
    public class JwtOptions : IJwtOptions
    {
        private const string Key = "lfLOpfp90L9kIdfu54s7f";

        public string Issuer { get; } = "ReadingList_API";

        public string Audience { get; } = "ReadingList_Client";

        public int Lifetime { get; } = 20;

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}