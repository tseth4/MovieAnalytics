namespace MovieAnalytics.API.Entities
{
    public class MovieStar
    {
        public required string MovieId { get; set; }
        public required Movie Movie { get; set; }
        public required int StarId { get; set; }
        public required Star Star { get; set; }
    }
}
