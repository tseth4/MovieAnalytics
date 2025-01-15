using Microsoft.EntityFrameworkCore;
using MovieAnalytics.Models.Domain;

namespace MovieAnalytics.Services
{
    public class MovieImportService
    {
        // Services/MovieImportService.cs
        public async Task ImportMovie(string[] row)
        {
            var movie = new Movie
            {
                Id = row[0],
                Title = row[1],
                // ... other properties
            };

            // Parse directors array from CSV (assuming it's in format: ['Director1', 'Director2'])
            var directorNames = row[12]  // adjust index based on your CSV
                .Trim('[', ']')
                .Split(',')
                .Select(d => d.Trim('\'', ' '))
                .Where(d => !string.IsNullOrEmpty(d));

            foreach (var directorName in directorNames)
            {
                // Find or create director
                var director = await _context.Directors
                    .FirstOrDefaultAsync(d => d.Name == directorName);

                if (director == null)
                {
                    director = new Director { Name = directorName };
                    _context.Directors.Add(director);
                }

                // Create movie-director relationship
                movie.MovieDirectors.Add(new MovieDirector
                {
                    Movie = movie,
                    Director = director
                });
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }
    }
}
