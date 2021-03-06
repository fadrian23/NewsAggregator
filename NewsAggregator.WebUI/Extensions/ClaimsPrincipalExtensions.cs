using System;
using System.Linq;
using System.Security.Claims;

namespace NewsAggregator.Services.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var claim = principal.Claims.SingleOrDefault(x => x.Type == "UserId")?.Value;
            return claim;
        }
    }
}
