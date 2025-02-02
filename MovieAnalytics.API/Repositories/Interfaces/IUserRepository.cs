using MovieAnalytics.API.DTOs;
using MovieAnalytics.API.Entities;
using MovieAnalytics.Helpers;

namespace MovieAnalytics.API.Repositories.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    // Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserByUsernameAsync(string username);
    Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
    Task<MemberDto?> GetMemberAsync(string username);
}