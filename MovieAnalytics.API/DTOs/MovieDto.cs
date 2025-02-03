using MovieAnalytics.API.Entities;

namespace MovieAnalytics.API.DTOs
{
    public class MovieDto
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string MovieLink { get; set; }
        public int? Year { get; set; }
        public string? Duration { get; set; }

        public string? MpaRating { get; set; }
        public decimal? Rating { get; set; }
        public string Votes { get; set; }
        public decimal Budget { get; set; }
        public decimal GrossWorldWide { get; set; }
        public decimal GrossUsCanada { get; set; }
        public decimal OpeningWeekendGross { get; set; }
        public int? Wins { get; set; }
        public int? Nominations { get; set; }
        public int? Oscars { get; set; }

        public List<string> DirectorNames { get; set; }
        public List<string> WriterNames { get; set; }
        public List<string> StarNames { get; set; }
        public List<string> GenreNames { get; set; }
        public List<string> CountryNames { get; set; }
        public List<string> ProductionCompanies { get; set; }
        public List<string> Languages { get; set; }
        
        public List<LikerDto> LikedBy { get; set; }



    }
}
