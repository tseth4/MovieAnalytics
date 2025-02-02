namespace MovieAnalytics.API.Entities
{
    public class Writer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<MovieWriter> MovieWriters { get; set; } = [];
    }
}
