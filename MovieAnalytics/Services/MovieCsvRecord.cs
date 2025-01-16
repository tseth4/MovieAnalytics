using CsvHelper.Configuration.Attributes;
using MovieAnalytics.Helpers;

namespace MovieAnalytics.Services
{
    public class MovieCsvRecord
    {
        [Name("id")]
        public required string Id { get; set; }

        [Name("Title")]
        public required string Title { get; set; }

        [Name("Movie_Link")]
        public string? MovieLink { get; set; }

        [Name("Year")]
        public int? Year { get; set; }

        [Name("Duration")]
        public string? Duration { get; set; }

        [Name("MPA")]
        public string? MpaRating { get; set; }

        [Name("Rating")]
        public decimal? Rating { get; set; }

        [Name("Votes")]
        public string? Votes { get; set; }

        [Name("budget")]
        [TypeConverter(typeof(ScientificDecimalConverter))]
        public decimal? Budget { get; set; }

        [Name("grossWorldWide")]
        public decimal? GrossWorldWide { get; set; }

        [Name("gross_US_Canada")]
        public decimal? GrossUsCanada { get; set; }

        [Name("opening_weekend_Gross")]
        public decimal? OpeningWeekendGross { get; set; }

        // These will be in format ['name1', 'name2']

        [Name("directors")]
        public string? Directors { get; set; }

        [Name("writers")]
        public string? Writers { get; set; }

        [Name("stars")]
        public string? Stars { get; set; }

        [Name("genres")]
        public string? Genres { get; set; }

        [Name("countries_origin")]
        public string? Countries { get; set; }

        [Name("production_companies")]
        public string? ProductionCompanies { get; set; }


        [Name("Languages")]
        public string? Languages { get; set; }

        [Name("wins")]
        public int? Wins { get; set; }

        [Name("nominations")]
        public int? Nominations { get; set; }

        [Name("oscars")]
        public int? Oscars { get; set; }

        // Helper method to parse array strings
        public string[] ParseArrayString(string arrayString)
        {
            if (string.IsNullOrEmpty(arrayString)) return Array.Empty<string>();

            return arrayString.Trim('[', ']')
                .Split(',')
                .Select(s => s.Trim('\'', ' '))
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();
        }

    }
}
