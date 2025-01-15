using Microsoft.EntityFrameworkCore;
using MovieAnalytics.Data;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Services.Interfaces;

namespace MovieAnalytics.Services
{
    public class MovieRepository : IMovieRepository
    {

        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Task<bool> AddAsync(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies
                 .Include(m => m.MovieDirectors)
                     .ThenInclude(md => md.Director)
                 .Include(m => m.MovieGenres)
                 .ToListAsync();
        }

        public Task<IEnumerable<Movie>> GetByGenreAsync(string genre)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
