using Microsoft.AspNetCore.Identity;

namespace MovieAnalytics.API.Entities;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; } = [];
}