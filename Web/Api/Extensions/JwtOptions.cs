using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions
{
    internal class JwtOptions : IJwtOptions
    {
        private static JwtOptions _jwtOptions;

        private JwtOptions()
        { }

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int Lifetime { get; set; }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

        public static JwtOptions GetInstance(IConfiguration configuration)
        {
            if (_jwtOptions != null) return _jwtOptions;
            _jwtOptions = new JwtOptions();
            configuration.Bind("JWT", _jwtOptions);
            return _jwtOptions;
        }
    }
}