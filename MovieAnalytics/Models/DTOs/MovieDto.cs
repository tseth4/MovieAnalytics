using MovieAnalytics.Models.Domain;

namespace MovieAnalytics.Models.DTOs
{
    public class MovieDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Rating { get; set; }
        public List<string> DirectorNames { get; set; }
        public List<string> WriterNames { get; set; }
        public List<string> StarNames { get; set; }
        public List<string> GenreNames { get; set; }
    }
}
