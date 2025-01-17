﻿using AutoMapper;
using MovieAnalytics.Models.Domain;
using MovieAnalytics.Models.DTOs;

namespace MovieAnalytics.Helpers
{



    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.DirectorNames, opt =>
                    opt.MapFrom(src => src.MovieDirectors
                        .Select(md => md.Director.Name)))
                .ForMember(dest => dest.GenreNames, opt =>
                    opt.MapFrom(src => src.MovieGenres
                        .Select(mg => mg.Genre.Name)))
                .ForMember(dest => dest.WriterNames, opt =>
                    opt.MapFrom(src => src.MovieWriters
                        .Select(mw => mw.Writer.Name)))
                .ForMember(dest => dest.StarNames, opt =>
                    opt.MapFrom(src => src.MovieStars
                        .Select(ms => ms.Star.Name)))
                .ForMember(dest => dest.CountryNames, opt =>
                    opt.MapFrom(src => src.MovieCountries
                        .Select(mc => mc.Country.Name)))
                .ForMember(dest => dest.ProductionCompanies, opt =>
                    opt.MapFrom(src => src.MovieProductionCompanies
                        .Select(mpc => mpc.ProductionCompany.Name)))
                .ForMember(dest => dest.Languages, opt =>
                    opt.MapFrom(src => src.MovieLanguages
                        .Select(ml => ml.Language.Name)));

            // For detailed view
            CreateMap<Movie, MovieDetailDto>()
                .ForMember(dest => dest.Directors, opt =>
                    opt.MapFrom(src => src.MovieDirectors
                        .Select(md => md.Director.Name)))
                .ForMember(dest => dest.Writers, opt =>
                    opt.MapFrom(src => src.MovieWriters
                        .Select(mw => mw.Writer.Name)))
                .ForMember(dest => dest.Stars, opt =>
                    opt.MapFrom(src => src.MovieStars
                        .Select(ms => ms.Star.Name)))
                .ForMember(dest => dest.Genres, opt =>
                    opt.MapFrom(src => src.MovieGenres
                        .Select(mg => mg.Genre.Name)));

            //// If you need to map in reverse direction
            //CreateMap<MovieCreateDto, Movie>();
            //CreateMap<MovieUpdateDto, Movie>();
        }
    }
}
