namespace MovieAnalytics.API.Entities
{
    public class MovieGenre
    {
        public required string MovieId { get; set; }
        public required Movie Movie { get; set; }
        public required int GenreId { get; set; }
        public required Genre Genre { get; set; }
    }
}
