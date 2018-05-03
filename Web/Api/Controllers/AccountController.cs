using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Api.Extensions;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IEnumerable<User> _users;
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            _users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "mike@mail.com",
                    Password = "123456",
                    Firstname = "Mikhail",
                    Lastname = "Siarko"
                },

                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "alex@mail.com",
                    Password = "654321",
                    Firstname = "Alexey",
                    Lastname = "Siarko"
                }
            };
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            // TODO Should put this functioanality to service
            var identity = GetIdentity(credentials.Email, credentials.Password, out var user);
            if (identity == null)
            {
                return BadRequest(new { errorMessage = "Invalid user name or password"});
            }

            var jwtOptions = JwtOptions.GetInstance(_configuration);
            var encodedJwt = EncodeSecurityToken(jwtOptions, identity);

            var response = new
            {
                token = encodedJwt,
                user = new
                {
                    email = user.Email,
                    firstname = user.Firstname,
                    lastname = user.Lastname
                }
            };

            return Json(response, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] UserCredentials credentials)
        {
            // Saving user

            var identity = GetIdentity(credentials.Email, credentials.Password, out var user);
            if (identity == null)
            {
                return BadRequest(new { errorMessage = "Invalid email or password" });
            }

            var jwtOptions = JwtOptions.GetInstance(_configuration);
            var encodedJwt = EncodeSecurityToken(jwtOptions, identity);

            var response = new
            {
                token = encodedJwt,
                user = new
                {
                    email = user.Email,
                    firstname = user.Firstname,
                    lastname = user.Lastname
                }
            };

            return Json(response, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
        }

        private ClaimsIdentity GetIdentity(string email, string password, out User user)
        {
            user = _users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user == null) return null;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;

        }

        private static string EncodeSecurityToken(IJwtOptions jwtOptions, ClaimsIdentity identity)
        {

            var jwt = GenerateToken(jwtOptions, identity.Claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwt);
        }

        private static JwtSecurityToken GenerateToken(IJwtOptions jwtOptions, IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            return new JwtSecurityToken(
                issuer : jwtOptions.Issuer,
                audience : jwtOptions.Audience,
                notBefore : now,
                claims : claims,
                expires : now.Add(TimeSpan.FromMinutes(jwtOptions.Lifetime)),
                signingCredentials : new SigningCredentials(jwtOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
        }
    }
}