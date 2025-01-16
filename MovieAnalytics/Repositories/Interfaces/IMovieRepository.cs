using MovieAnalytics.Models.Domain;

namespace MovieAnalytics.Repositories.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre);
        Task<IEnumerable<Movie>> GetMoviesByDirectorAsync(string directorName);
        Task<IEnumerable<Movie>> GetMoviesByYearAsync(int year);
        Task<Movie?> GetMovieWithAllRelationsAsync(string id);
    }
}
