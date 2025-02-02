using Microsoft.AspNetCore.Mvc;
using MovieAnalytics.API.DTOs.Analytics;
using MovieAnalytics.API.Services.Interfaces;

namespace MovieAnalytics.API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class AnalyticsController(IMovieAnalyticsService movieAnalyticsService) : BaseApiController
    {

        [HttpGet("budget-vs-gross/{countryName}")]
        public async Task<ActionResult<BudgetVsGrossChartDataDto>> GetBudgetVsGrossData(string countryName)
        {
            var chartData = await movieAnalyticsService.GetBudgetVsGrossChartDataAsync(countryName);
            return Ok(chartData);
        }

        [HttpGet("top-profitable/{countryName}")]
        public async Task<ActionResult<ChartDataDto>> GetTopProfitableMovies(string countryName)
        {
            var chartData = await movieAnalyticsService.GetTopProfitableMovies(countryName);
            return Ok(chartData);
        }
    }
}
