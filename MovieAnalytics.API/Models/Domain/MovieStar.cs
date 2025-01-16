namespace MovieAnalytics.Models.Domain
{
    public class MovieStar
    {
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
        public int StarId { get; set; }
        public Star Star { get; set; }
    }
}
