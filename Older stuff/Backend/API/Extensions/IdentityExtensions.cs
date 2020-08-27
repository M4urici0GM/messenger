using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace API.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == "UserId");
    
            return claim?.Value ?? string.Empty;
        }
    }
}