﻿using MovieAnalytics.Helpers;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Models.DTOs;

namespace MovieAnalytics.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre);
        Task<IEnumerable<Movie>> GetMoviesByDirectorAsync(string directorName);
        Task<IEnumerable<Movie>> GetMoviesByYearAsync(int year);
        Task<Movie?> GetMovieWithAllRelationsAsync(string id);

        Task<PagedList<MovieDto>> GetAllAsync(MovieParams movieParams);
        Task<Movie?> GetByIdAsync(string id);
    }
}