﻿using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using MovieAnalytics.Data;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Services.Interfaces;
using System.Formats.Asn1;
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
                        Votes = this.ParseVotes(record.Votes),
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
                        context.Directors,
                        name => new Director { Name = name },
                        director => new MovieDirector { Director = director },
                        (m, md) => m.MovieDirectors.Add(md)
                    );

                    await AddRelatedEntities(
                        movie,
                        record.ParseArrayString(record.Writers),
                        context.Writers,
                        name => new Writer { Name = name },
                        writer => new MovieWriter { Writer = writer },
                        (m, mw) => m.MovieWriters.Add(mw)
                    );

                    await AddRelatedEntities(
                        movie,
                        record.ParseArrayString(record.Stars),
                        context.Stars,
                        name => new Star { Name = name },
                        star => new MovieStar { Star = star },
                        (m, ms) => m.MovieStars.Add(ms)
                    );
                    // continue
                    await AddRelatedEntities(
                        movie,
                        record.ParseArrayString(record.Genres),
                        context.Genres,
                        name => new Genre { Name = name },
                        genre => new MovieGenre { Genre = genre },
                        (m, mg) => m.MovieGenres.Add(mg)
                    );

                    await AddRelatedEntities(
                            movie,
                            record.ParseArrayString(record.Countries),
                            context.Countries,
                            name => new Country { Name = name },
                            country => new MovieCountry { Country = country },
                            (m, mc) => m.MovieCountries.Add(mc)
                        );

                    await AddRelatedEntities(
                            movie,
                            record.ParseArrayString(record.ProductionCompanies),
                            context.ProductionCompanies,
                            name => new ProductionCompany { Name = name },
                            productionCompany => new MovieProductionCompany { ProductionCompany = productionCompany },
                            (m, mpc) => m.MovieProductionCompanies.Add(mpc)
                        );

                    await AddRelatedEntities(
                            movie,
                            record.ParseArrayString(record.Languages),
                            context.Languages,
                            name => new Language { Name = name },
                            lang => new MovieLanguage { Language = lang },
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

        private  int ParseVotes(string votes)
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
                if (decimal.TryParse (votes, out decimal number))
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
                    .FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);
                if (entity == null)
                {

                    entity = createEntity(name);
                    dbSet.Add(entity);
                }

                var junction = createJunction(entity);
                addJunction(movie, junction);
            }
        }

    }
}