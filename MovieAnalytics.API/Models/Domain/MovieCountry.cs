using System.Diagnostics.Metrics;

namespace MovieAnalytics.Models.Domain
{
    public class MovieCountry
    {
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
