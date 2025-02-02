using Microsoft.AspNetCore.Mvc;
using MovieAnalytics.API.DTOs;
using MovieAnalytics.API.Entities;
using MovieAnalytics.Extensions;
using MovieAnalytics.Helpers;
using MovieAnalytics.Repositories.Interfaces;

namespace MovieAnalytics.API.Controllers
{

    [ApiController]
    public class MoviesController(IMovieRepository movieRepository, ILogger<MoviesController> logger) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies([FromQuery] MovieParams movieParams)
        {

            logger.LogInformation("Fetching movies with parameters: {MovieParams}", movieParams);

            var movies = await movieRepository.GetAllAsync(movieParams);
            if (movies == null || !movies.Any())
            {
                logger.LogWarning("No movies found.");
                return NotFound("No movies found.");
            }
            Response.AddPaginationHeader(movies);

            return Ok(movies);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(string id)
        {
            var movie = await movieRepository.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }
        [HttpGet("byDirector/{directorName}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByDirector(string directorName)
        {
            var movies = await movieRepository.GetMoviesByDirectorAsync(directorName);
            return Ok(movies);
        }

        [HttpGet("byGenre/{genre}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByGenre(string genre)
        {
            var movies = await movieRepository.GetMoviesByGenreAsync(genre);
            return Ok(movies);
        }

        [HttpGet("byYear/{year}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByYear(int year)
        {
            var movies = await movieRepository.GetMoviesByYearAsync(year);
            return Ok(movies);
        }

    }
}
