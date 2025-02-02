namespace MovieAnalytics.API.Entities
{
    public class MovieWriter
    {
        public required string MovieId { get; set; }
        public required Movie Movie { get; set; }
        public required int WriterId { get; set; }
        public required Writer Writer { get; set; }
    }
}
