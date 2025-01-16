using Microsoft.EntityFrameworkCore;
using MovieAnalytics.Data;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Repositories.Interfaces;

namespace MovieAnalytics.Repositories
{
    public class MovieRepository(ApplicationDbContext context) : IMovieRepository
    {
        public async Task<bool> AddAsync(Movie entity)
        {
            await context.Movies.AddAsync(entity);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var movie = await context.Movies.FindAsync(id);
            if (movie == null) return false;

            context.Movies.Remove(movie);
            return await context.SaveChangesAsync() > 0;
        }


        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await context.Movies.ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(string id)  // Note the nullable return type
        {
            return await context.Movies.FindAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByDirectorAsync(string directorName)
        {
            return await context.Movies
                .Include(m => m.MovieDirectors)
                .ThenInclude(md => md.Director)
                .Where(m => m.MovieDirectors.Any(md => md.Director.Name == directorName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre)
        {
            return await context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Where(m => m.MovieGenres.Any(mg => mg.Genre.Name == genre))
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesByYearAsync(int year)
        {
            return await context.Movies.Where(m => m.Year == year).ToListAsync();
        }

        public async Task<Movie?> GetMovieWithAllRelationsAsync(string id)
        {
            return await context.Movies
                .Include(m => m.MovieDirectors)
                    .ThenInclude(md => md.Director)
                .Include(m => m.MovieWriters)
                    .ThenInclude(mw => mw.Writer)
                .Include(m => m.MovieStars)
                    .ThenInclude(ms => ms.Star)
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieCountries)
                    .ThenInclude(mc => mc.Country)
                .Include(m => m.MovieProductionCompanies)
                    .ThenInclude(mp => mp.ProductionCompany)
                .Include(m => m.MovieLanguages)
                    .ThenInclude(ml => ml.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> UpdateAsync(Movie entity)
        {
            context.Movies.Update(entity);
            return await context.SaveChangesAsync() > 0;
        }


    }
}
