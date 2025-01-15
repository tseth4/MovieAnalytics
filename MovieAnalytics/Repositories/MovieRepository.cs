using MovieAnalytics.Data;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Repositories.Interfaces;

namespace MovieAnalytics.Repositories
{
    public class MovieRepository(ApplicationDbContext context) : IMovieRepository
    {
        public Task<bool> AddAsync(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesByDirectorAsync(string directorName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesByYearAsync(int year)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieWithAllRelationsAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Movie entity)
        {
            throw new NotImplementedException();
        }
    }
}
