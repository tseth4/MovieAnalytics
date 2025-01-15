namespace MovieAnalytics.Models.Domain
{
    public class Writer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieWriter> MovieWriters { get; set; }
    }
}
