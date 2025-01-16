namespace MovieAnalytics.Models.Domain
{
    public class MovieProductionCompany
    {
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
        public int ProductionCompanyId { get; set; }
        public ProductionCompany ProductionCompany { get; set; }
    }
}
