using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieAnalytics.API.Data;
using MovieAnalytics.API.DTOs;
using MovieAnalytics.API.Entities;
using MovieAnalytics.API.Repositories.Interfaces;
using MovieAnalytics.Helpers;

namespace MovieAnalytics.API.Repositories;

public class LikesRepository(ApplicationDbContext context, IMapper mapper, ILogger<MovieRepository> logger) : ILikesRepository
{
    public async Task<Result<Unit>> AddLike(string? userId, string movieId)
    {

        var movie = await context.Movies.FindAsync(movieId);
        if (movie == null)
            return Result<Unit>.Failure($"Movie with ID {movieId} not found");

        // Then check for existing like
        var existingLike = await context.MovieLikes
            .FirstOrDefaultAsync(x => x.SourceUserId == userId && x.MovieId == movieId);
            
        if (existingLike != null)
            return Result<Unit>.Failure("You have already liked this movie");

        var like = new MovieLike
        {
            SourceUserId = userId,
            MovieId = movieId
        };

        context.MovieLikes.Add(like);

        try 
        {
            await context.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving like: {ex}");
            return Result<Unit>.Failure($"Failed to save like: {ex.Message}");
        } 
    }

    public async Task<PagedList<MovieDto>> GetUserLikes(string? userId, PaginationParams paginationParams)
    {
        var query = context.MovieLikes
            .Where(ml => ml.SourceUserId == userId)
            .Select(like => new MovieDto
            {
                Id = like.LikedMovie.Id,
                Title = like.LikedMovie.Title,
                Year = like.LikedMovie.Year,
                Duration = like.LikedMovie.Duration,
                MpaRating = like.LikedMovie.MpaRating,
                Rating = like.LikedMovie.Rating,
                // ... other movie properties you want to include
            })
            .AsNoTracking();

        return await PagedList<MovieDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
    }

    public async Task<PagedList<UserDto>> GetMovieLikes(string movieId, PaginationParams paginationParams)
    {
        var query = context.MovieLikes
            .Where(like => like.MovieId == movieId)
            .Select(like => new UserDto
            {
                Id = like.SourceUser.Id,
                Username = like.SourceUser.UserName,
                KnownAs = like.SourceUser.KnownAs,
            })
            .AsNoTracking();

        return await PagedList<UserDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
    }

    public async Task<Result<Unit>> RemoveLike(string? userId, string movieId)
    {
        if (userId == null) 
            return Result<Unit>.Failure("User ID is null");

        // First verify user exists
        var user = await context.Users.FindAsync(userId);
        if (user == null)
            return Result<Unit>.Failure("User not found");
        
        // Then verify movie exists
        var movie = await context.Movies.FindAsync(movieId);
        if (movie == null)
            return Result<Unit>.Failure("Movie not found");

        var like = await context.MovieLikes
            .FirstOrDefaultAsync(l => l.SourceUserId == userId && l.MovieId == movieId);

        if (like == null)
            return Result<Unit>.Failure("Like not found");

        context.MovieLikes.Remove(like);

        try 
        {
            await context.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure($"Failed to remove like: {ex.Message}");
        }
    }
}