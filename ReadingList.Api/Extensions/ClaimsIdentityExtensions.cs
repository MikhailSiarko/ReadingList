using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ReadingList.Api.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        public static int GetUserId(this IEnumerable<Claim> claims)
        {
            return int.Parse(claims.Single(c => c.Type == "Id").Value);
        }
    }
}