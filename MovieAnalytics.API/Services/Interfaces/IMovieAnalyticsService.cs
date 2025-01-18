using MovieAnalytics.API.Models.DTOs.Analytics;

namespace MovieAnalytics.API.Services.Interfaces
{
    public interface IMovieAnalyticsService
    {
        Task<ChartDataDto> GetBudgetVsGrossChartDataAsync();
        //Task<YearlyAggregationDto> GetAggregatedDataAsync();

    }
}
