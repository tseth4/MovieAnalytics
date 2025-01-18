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
using System.Diagnostics.Metrics;
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
                query = query.Where(m =>
                    m.Title.Contains(movieParams.SearchTerm) ||
                    m.MovieDirectors.Any(md => md.Director.Name.Contains(movieParams.SearchTerm)) ||
                    m.MovieGenres.Any(mg => mg.Genre.Name.Contains(movieParams.SearchTerm))
                );
            }

            return await PagedList<MovieDto>.CreateAsync(
                query.ProjectTo<MovieDto>(mapper.ConfigurationProvider),
                movieParams.PageNumber,
                movieParams.PageSize
            );
        }

        public async Task<List<YearlyAggregationDto>> GetBudgetVsGrossChartDataAsync(string countryName)
        {
            var stopwatch = Stopwatch.StartNew();

            var result = await context.Movies
            .Where(m =>
                m.MovieCountries.Any(mc => mc.Country.Name.Contains(countryName)) &&
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

        public async Task<List<MovieROIDto>> GetTopProfitableMovies(string countryName)
        {
            var stopwatch = Stopwatch.StartNew();

            var result = await context.Movies
                .Where(m => m.MovieCountries.Any(mc => mc.Country.Name.Contains(countryName)) &&
                m.Budget.HasValue &&
                m.GrossWorldWide.HasValue
                )
                .OrderByDescending(m => (double) (m.GrossWorldWide - m.Budget))
                .Take(10)
                .Select(m => new MovieROIDto
                {
                    Title = m.Title,
                    Budget = m.Budget,
                    GrossWorldWide = m.GrossWorldWide,
                    ROI = (m.GrossWorldWide - m.Budget) / m.Budget * 100
                }).ToListAsync();
            stopwatch.Stop();
            logger.LogInformation($"GetTopProfitableMovies executed in {stopwatch.ElapsedMilliseconds} ms.");
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
