using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ReadingList.Api.Authentication.AuthenticationOptions;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtOptions _jwtOptions;
        
        public AuthenticationService(IJwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }
        
        public static void ConfigureJwtBearer(JwtBearerOptions options)
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

        public string EncodeSecurityToken(User user)
        {
            var claimsIdentity = GetIdentity(user);
            var jwt = GenerateToken(_jwtOptions, claimsIdentity.Claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwt);
        }
        
        private static ClaimsIdentity GetIdentity(User user)
        {
            if (user == null)
                throw new ArgumentNullException();
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private static JwtSecurityToken GenerateToken(IJwtOptions jwtOptions, IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            return new JwtSecurityToken(
                jwtOptions.Issuer,
                jwtOptions.Audience,
                notBefore : now,
                claims : claims,
                expires : now.Add(TimeSpan.FromMinutes(jwtOptions.Lifetime)),
                signingCredentials : new SigningCredentials(jwtOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
        }

        public AuthenticationData GenerateAuthResponse(string token, User user) =>
            new AuthenticationData(token, user);
    }
}