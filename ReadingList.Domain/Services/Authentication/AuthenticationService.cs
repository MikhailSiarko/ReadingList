using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ReadingList.Api.Authentication.AuthenticationOptions;
using ReadingList.Domain.Queries;
using UserRm = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtOptions _jwtOptions;
        
        public AuthenticationService(IJwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        private string EncodeSecurityToken(UserRm user)
        {
            var claimsIdentity = GetIdentity(user);
            var jwt = GenerateToken(_jwtOptions, claimsIdentity.Claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwt);
        }
        
        private static ClaimsIdentity GetIdentity(UserRm user)
        {
            if (user == null)
                throw new ArgumentNullException();
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
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

        public AuthenticationData Authenticate(UserRm user, LoginUserQuery query)
        {
            var token = EncodeSecurityToken(user);
            return new AuthenticationData(token, user); 
        }
    }
}