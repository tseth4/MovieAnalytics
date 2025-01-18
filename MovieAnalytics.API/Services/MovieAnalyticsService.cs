using MovieAnalytics.API.Models.DTOs.Analytics;
using MovieAnalytics.API.Services.Interfaces;
using MovieAnalytics.Repositories.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovieAnalytics.API.Services
{
    public class MovieAnalyticsService(IMovieRepository movieRepository) : IMovieAnalyticsService
    {
        public async Task<BudgetVsGrossChartDataDto> GetBudgetVsGrossChartDataAsync(string countryName)
        {
            var aggregatedData = await movieRepository.GetAggregatedDataAsync(countryName);

            return new BudgetVsGrossChartDataDto
            {
                Labels = aggregatedData.Select(d => d.Year.ToString()).ToList(),
                AvgGrossValues = aggregatedData.Select(d => d.AvgGross).ToList(),
                AvgBudgetValues = aggregatedData.Select(d => d.AvgBudget).ToList(),
            };
        }
    }
}
