using MovieAnalytics.API.DTOs;
using MovieAnalytics.Helpers;

namespace MovieAnalytics.API.Repositories.Interfaces;

public interface ILikesRepository
{
    Task<Result<Unit>> AddLike(string? userId, string movieId);
    Task<PagedList<MovieDto>> GetUserLikes(string? userId, PaginationParams paginationParams);
    Task<PagedList<UserDto>> GetMovieLikes(string movieId, PaginationParams paginationParams);
    Task<Result<Unit>> RemoveLike(string? userId, string movieId);
}