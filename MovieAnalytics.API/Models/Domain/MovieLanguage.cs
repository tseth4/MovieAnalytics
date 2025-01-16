namespace MovieAnalytics.Models.Domain
{
    public class MovieLanguage
    {
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
