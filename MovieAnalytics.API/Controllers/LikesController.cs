using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieAnalytics.API.DTOs;
using MovieAnalytics.API.Entities;
using MovieAnalytics.API.Extensions;
using MovieAnalytics.API.Repositories.Interfaces;
using MovieAnalytics.Extensions;
using MovieAnalytics.Helpers;

namespace MovieAnalytics.API.Controllers;

public class LikesController(   
    ILikesRepository likesRepository,
    UserManager<AppUser> userManager,
    ILogger<LikesController> logger) :  BaseApiController
{


    [HttpGet("test")]
    public string Test()
    {
        return "test works!";
    }
    
    [Authorize]
    [HttpPost("{movieId}")]
    public async Task<ActionResult> AddLike(string movieId)
    {
        
        logger.LogInformation("Adding like for movie: {MovieId}", movieId);

        // Debug what's in the token
        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
        }
    
        var userId = User.GetUserId();
        Console.WriteLine($"UserId from GetUserId: {userId}");
    
        // Get the actual user for comparison
        var user = await userManager.GetUserAsync(User);
        Console.WriteLine($"User.Id from database: {user?.Id}");        
        
        var result = await likesRepository.AddLike(userId, movieId);
        
        if (!result.Succeeded)
        {
            logger.LogWarning("Failed to add like: {Error}", result.Error);
            return BadRequest(result.Error);
        }

        return Ok();
    }
    
    [Authorize]
    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetUserLikes([FromQuery] PaginationParams paginationParams)
    {
        var userId = User.GetUserId();
        logger.LogInformation("Fetching likes for user: {UserId}", userId);

        var likedMovies = await likesRepository.GetUserLikes(userId, paginationParams);
        
        if (!likedMovies.Any())
        {
            logger.LogInformation("No liked movies found for user: {UserId}", userId);
            return Ok(new List<MovieDto>());
        }

        Response.AddPaginationHeader(likedMovies);
        return Ok(likedMovies);
    }

    [HttpGet("movie/{movieId}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetMovieLikes(string movieId, [FromQuery] PaginationParams paginationParams)
    {
        logger.LogInformation("Fetching likes for movie: {MovieId}", movieId);

        var userLikes = await likesRepository.GetMovieLikes(movieId, paginationParams);
        
        if (!userLikes.Any())
        {
            logger.LogInformation("No likes found for movie: {MovieId}", movieId);
            return Ok(new List<UserDto>());
        }

        Response.AddPaginationHeader(userLikes);
        return Ok(userLikes);
    }

    [Authorize]
    [HttpDelete("{movieId}")]
    public async Task<ActionResult> RemoveLike(string movieId)
    {
        logger.LogInformation("Removing like for movie: {MovieId}", movieId);

        var userId = User.GetUserId();
        var result = await likesRepository.RemoveLike(userId, movieId);

        if (!result.Succeeded)
        {
            logger.LogWarning("Failed to remove like: {Error}", result.Error);
            return BadRequest(result.Error);
        }

        return Ok();
    }
}