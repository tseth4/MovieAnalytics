using Microsoft.AspNetCore.Mvc;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Repositories.Interfaces;

namespace MovieAnalytics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController(IMovieRepository movieRepository) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await movieRepository.GetAllAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(string id)
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

        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(Movie movie)
        {
            var success = await movieRepository.AddAsync(movie);
            if (!success)
                return BadRequest();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(string id, Movie movie)
        {
            if (id != movie.Id)
                return BadRequest();

            var success = await movieRepository.UpdateAsync(movie);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(string id)
        {
            var success = await movieRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

    }
}
