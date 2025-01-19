namespace MovieAnalytics.Models.Domain
{
    public class MovieDirector
    {
        public required string MovieId { get; set; }
        public required Movie Movie { get; set; }
        public required int DirectorId { get; set; }
        public required Director Director { get; set; }
    }
}
