using System.Linq;
using System.Security.Claims;

namespace ReadingList.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.Claims.Single(c => c.Type == "Id").Value);
        }
    }
}