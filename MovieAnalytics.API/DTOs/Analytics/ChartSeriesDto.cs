namespace MovieAnalytics.API.DTOs.Analytics
{
    public class ChartSeriesDto
    {
        public required string Name { get; set; }
        public required List<decimal> Values { get; set; }
    }
}
