namespace MovieAnalytics.API.Entities
{
    public class MovieProductionCompany
    {
        public required string MovieId { get; set; }
        public required Movie Movie { get; set; }
        public required int ProductionCompanyId { get; set; }
        public required ProductionCompany ProductionCompany { get; set; }
    }
}
