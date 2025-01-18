using MovieAnalytics.API.Models.DTOs.Analytics;

namespace MovieAnalytics.API.Services.Interfaces
{
    public interface IMovieAnalyticsService
    {
        Task<BudgetVsGrossChartDataDto> GetBudgetVsGrossChartDataAsync(string countryName);
        //Task<YearlyAggregationDto> GetAggregatedDataAsync();

    }
}
