namespace MovieAnalytics.API.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<MovieLanguage> MovieLanguages { get; set; } = [];
    }
}
