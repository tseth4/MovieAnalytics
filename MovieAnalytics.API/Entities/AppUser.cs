using Microsoft.AspNetCore.Identity;

namespace MovieAnalytics.API.Entities;

public class AppUser : IdentityUser
{
    public string KnownAs { get; set; }  
    public List<MovieLike> LikedMovies { get; set; }

}