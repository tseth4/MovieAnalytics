namespace MovieAnalytics.API.Models.DTOs.Analytics
{
    public class MovieROIDto
    {
        public string Title { get; set; }
        public decimal? Budget { get; set; }
        public decimal? GrossWorldWide { get; set; }
        public decimal? ROI { get; set; }

    }
}
