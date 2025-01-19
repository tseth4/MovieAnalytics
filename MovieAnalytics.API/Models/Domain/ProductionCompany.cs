namespace MovieAnalytics.Models.Domain
{
    public class ProductionCompany
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<MovieProductionCompany> MovieProductionCompanies { get; set; } = [];
    }
}
