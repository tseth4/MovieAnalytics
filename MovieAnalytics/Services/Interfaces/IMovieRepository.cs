using MovieAnalytics.Models.Domain;

namespace MovieAnalytics.Services.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(string id);
        Task<IEnumerable<Movie>> GetByGenreAsync(string genre);
        Task<bool> AddAsync(Movie movie);
        Task<bool> UpdateAsync(Movie movie);
        Task<bool> DeleteAsync(string id);
    }
}
