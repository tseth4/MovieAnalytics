namespace MovieAnalytics.API.Entities
{
    public class ProductionCompany
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<MovieProductionCompany> MovieProductionCompanies { get; set; } = [];
    }
}
