namespace MovieAnalytics.Services.Interfaces
{
    public interface IMovieImportService
    {
        Task ImportMoviesFromCsv(string csvContent);

    }
}
