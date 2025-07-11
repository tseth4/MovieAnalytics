﻿using System.Diagnostics;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieAnalytics.API.Data;
using MovieAnalytics.API.DTOs;
using MovieAnalytics.API.DTOs.Analytics;
using MovieAnalytics.API.Entities;
using MovieAnalytics.Helpers;
using MovieAnalytics.Repositories.Interfaces;

namespace MovieAnalytics.API.Repositories
{
    public class MovieRepository(ApplicationDbContext context, IMapper mapper, ILogger<MovieRepository> logger)
        : IMovieRepository
    {
        public async Task<PagedList<MovieDto>> GetAllAsync(MovieParams movieParams)
        {
            var searchTerm = movieParams.SearchTerm?.ToLower() ?? "";

            var query = context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(m =>
                    EF.Functions.Like(m.Title.ToLower(), $"%{searchTerm}%") ||
                    m.MovieDirectors.Select(md => md.Director.Name.ToLower()).Any(name => name.Contains(searchTerm)) ||
                    m.MovieGenres.Select(mg => mg.Genre.Name.ToLower()).Any(name => name.Contains(searchTerm))
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

            var movies = await context.Movies
                .Where(m =>
                    m.MovieCountries.Any(mc => mc.Country.Name.Contains(countryName)) &&
                    m.Year.HasValue &&
                    m.Budget.HasValue && m.Budget > 0 &&
                    m.GrossWorldWide.HasValue && m.GrossWorldWide > 0
                )
                .ToListAsync(); // Pull to memory

            var result = movies
                .GroupBy(m => m.Year.Value)
                .Select(g => new YearlyAggregationDto
                {
                    Year = g.Key,
                    AvgBudget = (decimal)g.Average(m => (double)m.Budget.Value),
                    AvgGross = (decimal)g.Average(m => (double)m.GrossWorldWide.Value)
                }).ToList();


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
                .OrderByDescending(m => (double)(m.GrossWorldWide - m.Budget))
                .Take(10)
                .Select(m => new MovieROIDto
                {
                    Title = m.Title,
                    Budget = m.Budget,
                    GrossWorldWide = m.GrossWorldWide,
                    ROI = (double)(m.GrossWorldWide.Value - m.Budget.Value) / (double)m.Budget.Value * 100
                }).ToListAsync();
            stopwatch.Stop();
            logger.LogInformation($"GetTopProfitableMovies executed in {stopwatch.ElapsedMilliseconds} ms.");
            return result;
        }

        public async Task<MovieDto?> GetByIdAsync(string id) // Note the nullable return type
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