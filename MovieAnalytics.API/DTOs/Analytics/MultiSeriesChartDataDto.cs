namespace MovieAnalytics.API.DTOs.Analytics
{
    public class MultiSeriesChartDataDto
    {
        public required List<string> Labels { get; set; }
        public required List<ChartSeriesDto> SeriesDtos { get; set; }
    }
}
