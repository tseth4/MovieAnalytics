namespace MovieAnalytics.API.Entities
{
    public class MovieLanguage
    {
        public required string MovieId { get; set; }
        public required Movie Movie { get; set; }
        public required int LanguageId { get; set; }
        public required Language Language { get; set; }
    }
}
