namespace MovieAnalytics.API.DTOs.Analytics
{
    public class ChartDataDto
    {
        public required List<string> Labels { get; set; }
        public required List<double> Values { get; set; }
    }
}
