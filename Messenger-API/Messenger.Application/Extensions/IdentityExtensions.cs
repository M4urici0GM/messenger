using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Messenger.Domain.Structs;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Messenger.Application.Extensions
{
    public static class IdentityExtensions
    {
        public static Guid? GetUserId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == JwtClaimNames.UserId);

            string claimString = claim?.Value;

            return !string.IsNullOrEmpty(claimString)
                ? Guid.Parse(claimString)
                : (Guid?) null;
        }
    }
}