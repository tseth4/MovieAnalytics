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
            var aggregatedData = await movieRepository.GetBudgetVsGrossChartDataAsync(countryName);

            return new BudgetVsGrossChartDataDto
            {
                Labels = aggregatedData.Select(d => d.Year.ToString()).ToList(),
                AvgGrossValues = aggregatedData.Select(d => d.AvgGross).ToList(),
                AvgBudgetValues = aggregatedData.Select(d => d.AvgBudget).ToList(),
            };
        }

        public async Task<ChartDataDto> GetTopProfitableMovies(string countryName)
        {   var movieRoiData = await movieRepository.GetTopProfitableMovies(countryName);
            return new ChartDataDto
            {
                Labels = movieRoiData.Select(m => m.Title).ToList(),
                Values = movieRoiData.Select(m => m.ROI ?? 0).ToList()
            };
        }
    }

}
