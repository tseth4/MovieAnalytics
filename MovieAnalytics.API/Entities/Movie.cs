namespace MovieAnalytics.API.Entities
{
    public class Movie
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public string? MovieLink { get; set; }
        public int? Year { get; set; }
        public string? Duration { get; set; }
        public string? MpaRating { get; set; }
        public decimal? Rating { get; set; }
        public int? Votes { get; set; }
        public decimal? Budget { get; set; }
        public decimal? GrossWorldWide { get; set; }
        public decimal? GrossUsCanada { get; set; }
        public decimal? OpeningWeekendGross { get; set; }
        public int? Wins { get; set; }
        public int? Nominations { get; set; }
        public int? Oscars { get; set; }

        // Navigation properties
        public ICollection<MovieDirector> MovieDirectors { get; set; } = [];
        public ICollection<MovieWriter> MovieWriters { get; set; } = [];
        public ICollection<MovieStar> MovieStars { get; set; } = [];
        public ICollection<MovieGenre> MovieGenres { get; set; } = [];
        public ICollection<MovieCountry> MovieCountries { get; set; } = [];
        public ICollection<MovieProductionCompany> MovieProductionCompanies { get; set; } = [];
        public ICollection<MovieLanguage> MovieLanguages { get; set; } = [];
    }
}
