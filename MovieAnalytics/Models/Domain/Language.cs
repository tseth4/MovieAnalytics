namespace MovieAnalytics.Models.Domain
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieLanguage> MovieLanguages { get; set; }
    }
}
