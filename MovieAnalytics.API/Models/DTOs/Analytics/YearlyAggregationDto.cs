namespace MovieAnalytics.API.Models.DTOs.Analytics
{
    public class YearlyAggregationDto
    {
        public int Year { get; set; }
        public decimal AvgBudget { get; set; }
        public decimal AvgGross { get; set; }
    }
}
