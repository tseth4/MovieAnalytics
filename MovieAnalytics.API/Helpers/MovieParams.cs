namespace MovieAnalytics.Helpers
{
    public class MovieParams : PaginationParams
    {
        public string? SearchTerm { get; set; }
        public int? Year { get; set; }
        public string? Genre { get; set; }
        public decimal? MinRating { get; set; }
        public decimal? MaxRating { get; set; }
    }
}
