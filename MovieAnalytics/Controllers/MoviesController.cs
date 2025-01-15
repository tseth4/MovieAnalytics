using Microsoft.AspNetCore.Mvc;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Services.Interfaces;

namespace MovieAnalytics.Controllers
{
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _movieRepository.GetAllAsync();
            return Ok(movies);
        }

    }
}
