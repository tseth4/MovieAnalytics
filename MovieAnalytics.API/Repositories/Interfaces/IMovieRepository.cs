using MovieAnalytics.API.Models.DTOs.Analytics;
using MovieAnalytics.Helpers;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Models.DTOs;

namespace MovieAnalytics.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre);
        Task<IEnumerable<Movie>> GetMoviesByDirectorAsync(string directorName);
        Task<IEnumerable<Movie>> GetMoviesByYearAsync(int year);
        Task<Movie?> GetMovieWithAllRelationsAsync(string id);

        Task<PagedList<MovieDto>> GetAllAsync(MovieParams movieParams);

        Task<MovieDto?> GetByIdAsync(string id);

        //Task<List<MovieDto>> GetAllForAnalyticsAsync();

        Task<List<YearlyAggregationDto>> GetBudgetVsGrossChartDataAsync(string countryName);
        Task<List<MovieROIDto>> GetTopProfitableMovies(string countryName);
    }
}
