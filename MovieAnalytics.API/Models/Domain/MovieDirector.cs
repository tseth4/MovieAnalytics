namespace MovieAnalytics.Models.Domain
{
    public class MovieDirector
    {
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
    }
}
