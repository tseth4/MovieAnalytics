namespace MovieAnalytics.Models.Domain
{
    public class Star
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<MovieStar> MovieStars { get; set; } = [];
    }
}
