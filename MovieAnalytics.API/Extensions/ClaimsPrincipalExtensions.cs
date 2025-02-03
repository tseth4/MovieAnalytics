using System.Security.Claims;

namespace MovieAnalytics.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        // var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)
        //              ?? throw new Exception("Cannot get userid from token");
        //
        // return userId;
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    }
}