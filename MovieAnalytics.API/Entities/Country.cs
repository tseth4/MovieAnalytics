namespace MovieAnalytics.API.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<MovieCountry> MovieCountries { get; set; } = [];
    }
}
