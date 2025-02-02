namespace MovieAnalytics.API.DTOs
{
    public class MovieDetailDto
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public string? MovieLink { get; set; }
        public int Year { get; set; }
        public string? Duration { get; set; }
        public string? MpaRating { get; set; }
        public decimal Rating { get; set; }
        public int Votes { get; set; }
        public decimal Budget { get; set; }
        public decimal GrossWorldWide { get; set; }
        public List<string>? Directors { get; set; }
        public List<string>? Writers { get; set; }
        public List<string>? Stars { get; set; }
        public List<string>? Genres { get; set; }
    }
}
