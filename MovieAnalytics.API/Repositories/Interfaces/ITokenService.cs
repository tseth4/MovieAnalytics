using MovieAnalytics.API.Entities;

namespace MovieAnalytics.API.Repositories.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}