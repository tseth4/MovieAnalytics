using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieAnalytics.API.Models.DTOs.Analytics;
using MovieAnalytics.Data;
using MovieAnalytics.Helpers;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Models.DTOs;
using MovieAnalytics.Repositories.Interfaces;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieAnalytics.Repositories
{
    public class MovieRepository(ApplicationDbContext context, IMapper mapper, ILogger<MovieRepository> logger) : IMovieRepository
    {

        public async Task<PagedList<MovieDto>> GetAllAsync(MovieParams movieParams)
        {

            var query = context.Movies.AsQueryable();


            if (!string.IsNullOrEmpty(movieParams.SearchTerm))
            {
                query = query.Where(m => m.Title.Contains(movieParams.SearchTerm));
            }

            if (movieParams.Year.HasValue)
            {
                query = query.Where(m => m.Year == movieParams.Year);
            }

            if (!string.IsNullOrEmpty(movieParams.Genre))
            {
                query = query.Where(m => m.MovieGenres.Any(mg =>
                    mg.Genre.Name == movieParams.Genre));
            }

            if (movieParams.MinRating.HasValue)
            {
                query = query.Where(m => m.Rating >= movieParams.MinRating);
            }
            return await PagedList<MovieDto>.CreateAsync(
                query.ProjectTo<MovieDto>(mapper.ConfigurationProvider),
                movieParams.PageNumber,
                movieParams.PageSize
            );
        }

        public async Task<List<YearlyAggregationDto>> GetAggregatedDataAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            var result = await context.Movies
                .Where(m => 
                m.Year.HasValue &&
                m.Budget.HasValue && m.Budget > 0 && 
                m.GrossWorldWide.HasValue && m.GrossWorldWide > 0
                )
                .GroupBy(m => m.Year.Value)
                .Select(g => new YearlyAggregationDto
                {
                    Year = g.Key,
                    AvgBudget = g.Average(m => m.Budget.Value),
                    AvgGross = g.Average(m => m.GrossWorldWide.Value)
                }).ToListAsync();


            stopwatch.Stop();
            logger.LogInformation($"GetAggregatedDataAsync executed in {stopwatch.ElapsedMilliseconds} ms.");
            return result;
        }

        public async Task<MovieDto?> GetByIdAsync(string id)  // Note the nullable return type
        {
            var query = context.Movies.AsQueryable();

            return await query
                .ProjectTo<MovieDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<IEnumerable<Movie>> GetMoviesByDirectorAsync(string directorName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesByYearAsync(int year)
        {
            throw new NotImplementedException();
        }

        public Task<Movie?> GetMovieWithAllRelationsAsync(string id)
        {
            throw new NotImplementedException();
        }


    }
}
