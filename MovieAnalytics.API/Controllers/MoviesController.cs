using Microsoft.AspNetCore.Mvc;
using MovieAnalytics.Extensions;
using MovieAnalytics.Helpers;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Models.DTOs;
using MovieAnalytics.Repositories.Interfaces;

namespace MovieAnalytics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController(IMovieRepository movieRepository) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies([FromQuery] MovieParams movieParams)
        {
            var movies = await movieRepository.GetAllAsync(movieParams);
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
