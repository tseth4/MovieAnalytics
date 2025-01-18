namespace MovieAnalytics.API.Models.DTOs.Analytics
{
    public class ChartDataDto
    {
        public List<string> Labels { get; set; }
        public List<decimal> Values { get; set; }
    }
}
