namespace MovieAnalytics.Models.Domain
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieCountry> MovieCountries { get; set; }
    }
}
