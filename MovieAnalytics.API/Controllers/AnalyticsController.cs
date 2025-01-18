using Microsoft.AspNetCore.Mvc;
using MovieAnalytics.API.Models.DTOs.Analytics;
using MovieAnalytics.API.Services.Interfaces;

namespace MovieAnalytics.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController(IMovieAnalyticsService movieAnalyticsService) : ControllerBase
    {

        [HttpGet("budget-vs-gross")]
        public async Task<ActionResult<ChartDataDto>> GetBudgetVsGrossData()
        {
            var chartData = await movieAnalyticsService.GetBudgetVsGrossChartDataAsync();
            return Ok(chartData);
        }

    }
}
