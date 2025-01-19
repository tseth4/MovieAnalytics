using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using MovieAnalytics.Data;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Services.Interfaces;
using System.Globalization;

namespace MovieAnalytics.Services
{
    public class MovieImportService(ApplicationDbContext context) : IMovieImportService
    {
        public async Task ImportMoviesFromCsv(string csvPath)
        {
            var csvContent = File.ReadAllText(csvPath); // Ensure the file content is read
            using var reader = new StringReader(csvContent);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                // Case-insensitive, trim spaces
                PrepareHeaderForMatch = args => args.Header.Trim().ToLowerInvariant(),
                // Ignore missing headers (optional)
                HeaderValidated = null
            };

            try
            {
                using var csv = new CsvReader(reader, config);

                // Log headers for debugging
                csv.Read();
                csv.ReadHeader();
                Console.WriteLine($"Detected Headers: {string.Join(", ", csv.HeaderRecord)}");

                var records = csv.GetRecords<MovieCsvRecord>().ToList();

                foreach (var record in records)
                {
                    Console.WriteLine($"Processing movie: {record.Title}");
                    // Add your processing logic here


                    var movie = new Movie
                    {
                        Id = record.Id,
                        Title = record.Title,
                        MovieLink = record.MovieLink,
                        Year = record.Year,
                        Duration = record.Duration,
                        MpaRating = record.MpaRating,
                        Rating = record.Rating,
                        Votes = this.ParseVotes(votes: record.Votes),
                        Budget = record.Budget,
                        GrossWorldWide = record.GrossWorldWide,
                        GrossUsCanada = record.GrossUsCanada,
                        OpeningWeekendGross = record.OpeningWeekendGross,
                        Wins = record.Wins,
                        Nominations = record.Nominations,
                        Oscars = record.Oscars
                    };

                    // For Directors
                    await AddRelatedEntities(
                         movie,
                         record.ParseArrayString(record.Directors),
                         context,
                         context.Directors,
                         name => new Director { Name = name },
                         director => new MovieDirector { DirectorId = director.Id, MovieId = movie.Id, Director = director, Movie = movie },
                         (m, md) => m.MovieDirectors.Add(md)
                    );
                    await AddRelatedEntities(
                        movie,
                        record.ParseArrayString(record.Writers),
                        context,
                        context.Writers,
                        name => new Writer { Name = name },
                        writer => new MovieWriter { WriterId = writer.Id, MovieId = movie.Id, Writer = writer, Movie = movie },
                        (m, mw) => m.MovieWriters.Add(mw)
                    );

                    await AddRelatedEntities(
                        movie,
                        record.ParseArrayString(record.Stars),
                        context,
                        context.Stars,
                        name => new Star { Name = name },
                        star => new MovieStar { StarId = star.Id, MovieId = movie.Id, Star = star, Movie = movie },
                        (m, ms) => m.MovieStars.Add(ms)
                    );
                    // continue
                    await AddRelatedEntities(
                        movie,
                        record.ParseArrayString(record.Genres),
                        context,
                        context.Genres,
                        name => new Genre { Name = name },
                        genre => new MovieGenre { GenreId = genre.Id, MovieId = movie.Id, Genre = genre, Movie = movie },
                        (m, mg) => m.MovieGenres.Add(mg)
                    );

                    await AddRelatedEntities(
                         movie,
                         record.ParseArrayString(record.Countries),
                         context,
                         context.Countries,
                         name => new Country { Name = name },
                         country => new MovieCountry { CountryId = country.Id, MovieId = movie.Id, Country = country, Movie = movie },
                         (m, mc) => m.MovieCountries.Add(mc)
                     );

                    await AddRelatedEntities(
                        movie,
                        record.ParseArrayString(record.ProductionCompanies),
                        context,
                        context.ProductionCompanies,
                        name => new ProductionCompany { Name = name },
                        productionCompany => new MovieProductionCompany { ProductionCompanyId = productionCompany.Id, MovieId = movie.Id, ProductionCompany = productionCompany, Movie = movie },
                        (m, mpc) => m.MovieProductionCompanies.Add(mpc)
                    );

                    await AddRelatedEntities(
                        movie,
                        record.ParseArrayString(record.Languages),
                        context,
                        context.Languages,
                        name => new Language { Name = name },
                        lang => new MovieLanguage { LanguageId = lang.Id, MovieId = movie.Id, Language = lang, Movie = movie },
                        (m, ml) => m.MovieLanguages.Add(ml)
                    );

                    await context.Movies.AddAsync(movie);


                }
            }
            catch (HeaderValidationException ex)
            {
                Console.WriteLine($"Header validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during data import: {ex.Message}");
            }



            await context.SaveChangesAsync();
        }

        private int ParseVotes(string votes)
        {
            if (string.IsNullOrWhiteSpace(votes))
            {
                Console.WriteLine("Input is null or empty");
            }


            votes = votes.Replace(",", "");

            if (votes.EndsWith("K", StringComparison.OrdinalIgnoreCase))
            {
                votes = votes.TrimEnd('K', 'k');
                if (decimal.TryParse(votes, out decimal number))
                {
                    return (int)(number * 1000);
                }

            }

            if (votes.EndsWith("M", StringComparison.OrdinalIgnoreCase))
            {
                votes = votes.TrimEnd('M', 'm');
                if (decimal.TryParse(votes, out decimal number))
                {
                    return (int)(number * 1000000);
                }
            }

            return int.TryParse(votes, out int result) ? result : 0;
        }

        // Generic helper method for adding related entities
        private async Task AddRelatedEntities<TEntity, TJunction>(
            Movie movie,
            string[] names,
            DbContext context, // Added DbContext as a parameter
            DbSet<TEntity> dbSet,
            Func<string, TEntity> createEntity,
            Func<TEntity, TJunction> createJunction,
            Action<Movie, TJunction> addJunction
            )
            where TEntity : class
            where TJunction : class
        {
            foreach (var name in names)
            {
                var entity = await dbSet
                    .AsNoTracking() // Improves performance by not tracking if entity exists
                    .FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);
                if (entity == null)
                {
                    // Create a new entity and save it to the database to generate the ID
                    entity = createEntity(name);
                    dbSet.Add(entity);
                    //await context.SaveChangesAsync(); // Ensure ID is generated
                }
                else
                {
                    dbSet.Attach(entity);
                }

                var junction = createJunction(entity);


                addJunction(movie, junction);
            }
        }

    }
}
