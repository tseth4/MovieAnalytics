using MovieAnalytics.API.Models.DTOs.Analytics;
using MovieAnalytics.API.Services.Interfaces;
using MovieAnalytics.Repositories.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovieAnalytics.API.Services
{
    public class MovieAnalyticsService(IMovieRepository movieRepository) : IMovieAnalyticsService
    {
        public async Task<ChartDataDto> GetBudgetVsGrossChartDataAsync()
        {
            var aggregatedData = await movieRepository.GetAggregatedDataAsync();

            return new ChartDataDto
            {
                Labels = aggregatedData.Select(d => d.Year.ToString()).ToList(),
                Values = aggregatedData.Select(d => d.AvgGross).ToList(),
            };
        }
    }
}
