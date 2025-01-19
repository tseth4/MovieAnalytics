namespace MovieAnalytics.Models.Domain
{
    public class MovieCountry
    {
        public required string MovieId { get; set; }
        public required Movie Movie { get; set; }
        public required int CountryId { get; set; }
        public required Country Country { get; set; }
    }
}
