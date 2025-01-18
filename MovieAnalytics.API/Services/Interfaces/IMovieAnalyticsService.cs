using MovieAnalytics.API.Models.DTOs.Analytics;
using System.Threading.Tasks;

namespace MovieAnalytics.API.Services.Interfaces
{
    public interface IMovieAnalyticsService
    {
        Task<BudgetVsGrossChartDataDto> GetBudgetVsGrossChartDataAsync(string countryName);
        //Task<YearlyAggregationDto> GetAggregatedDataAsync();
        Task<ChartDataDto> GetTopProfitableMovies(string countryName);

    }
}
