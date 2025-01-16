namespace MovieAnalytics.API.Models.DTOs.Analytics
{
    public class YearlyAnalyticsDto
    {
        public int Year { get; set; }
        public int MovieCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageRating { get; set; }
        public decimal AverageBudget { get; set; }
    }
}
