namespace MovieAnalytics.API.Entities
{
    public class Director
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<MovieDirector> MovieDirectors { get; set; } = [];
    }
}
