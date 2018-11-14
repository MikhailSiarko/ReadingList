using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ReadingList.Domain.Models.DTO.User;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtOptions _jwtOptions;
        
        public AuthenticationService(IJwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        private string EncodeSecurityToken(UserDto user)
        {
            var claimsIdentity = GetIdentity(user);
            var jwt = GenerateToken(_jwtOptions, claimsIdentity.Claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwt);
        }
        
        private static ClaimsIdentity GetIdentity(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private static JwtSecurityToken GenerateToken(IJwtOptions jwtOptions, IEnumerable<Claim> claims)
        {
            if (jwtOptions == null)
                throw new ArgumentNullException(nameof(jwtOptions));
            
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

        public AuthenticationData Authenticate(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            
            var token = EncodeSecurityToken(user);
            var userIdentityDto = Mapper.Map<UserDto, UserIdentityDto>(user);
            return new AuthenticationData(token, userIdentityDto); 
        }
    }
}