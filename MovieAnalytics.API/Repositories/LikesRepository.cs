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
        // Log the incoming IDs
        Console.WriteLine($"Attempting to add like - UserId: {userId}, MovieId: {movieId}");

        // First verify the movie exists
        var movie = await context.Movies.FindAsync(movieId);
        Console.WriteLine($"Movie exists? {movie != null}");
        if (movie == null)
            return Result<Unit>.Failure($"Movie with ID {movieId} not found");

        // Then check for existing like
        var existingLike = await context.MovieLikes
            .FirstOrDefaultAsync(x => x.SourceUserId == userId && x.MovieId == movieId);
        Console.WriteLine($"Like already exists? {existingLike != null}");
            
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

    public Task<PagedList<MovieDto>> GetUserLikes(string? userId, PaginationParams paginationParams)
    {
        throw new NotImplementedException();
    }

    public Task<PagedList<UserDto>> GetMovieLikes(string movieId, PaginationParams paginationParams)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Unit>> RemoveLike(string? userId, string movieId)
    {
        throw new NotImplementedException();
    }
}