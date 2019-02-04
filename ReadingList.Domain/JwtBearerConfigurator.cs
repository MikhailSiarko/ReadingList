using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ReadingList.Domain.Services.Authentication;

namespace ReadingList.Domain
{
    public static class JwtBearerConfigurator
    {
        public static void Configure(JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = ConfigureTokenValidationParameters();
        }

        private static TokenValidationParameters ConfigureTokenValidationParameters()
        {
            var jwtOptions = new JwtOptions();
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
            };
        }
    }
}