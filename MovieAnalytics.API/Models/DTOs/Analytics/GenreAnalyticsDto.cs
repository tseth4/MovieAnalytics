namespace MovieAnalytics.API.Models.DTOs.Analytics
{
    public class GenreAnalyticsDto
    {
        public string Genre { get; set; }
        public int MovieCount { get; set; }
        public decimal AverageRating { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageBudget { get; set; }
        public double ROI { get; set; }  // Return on Investment
    }
}
