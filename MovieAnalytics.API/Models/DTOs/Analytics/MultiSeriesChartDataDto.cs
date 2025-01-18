namespace MovieAnalytics.API.Models.DTOs.Analytics
{
    public class MultiSeriesChartDataDto
    {
        public List<string> Labels { get; set; }
        public List<ChartSeriesDto> SeriesDtos { get; set; }
    }
}
