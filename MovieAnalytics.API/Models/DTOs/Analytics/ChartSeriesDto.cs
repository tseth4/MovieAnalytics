namespace MovieAnalytics.API.Models.DTOs.Analytics
{
    public class ChartSeriesDto
    {
        public string Name { get; set; }
        public List<decimal> Values { get; set; }
    }
}
