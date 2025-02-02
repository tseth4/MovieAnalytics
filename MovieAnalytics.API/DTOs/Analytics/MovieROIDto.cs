namespace MovieAnalytics.API.DTOs.Analytics
{
    public class MovieROIDto
    {
        public required string Title { get; set; }
        public decimal? Budget { get; set; }
        public decimal? GrossWorldWide { get; set; }
        public decimal? ROI { get; set; }

    }
}
