using AutoMapper;
using MovieAnalytics.API.DTOs;
using MovieAnalytics.API.Entities;

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
                        .Select(ml => ml.Language.Name)))
                .ForMember(dest => dest.LikedBy, opt =>
                    opt.MapFrom(src => src.LikedByUsers.Select(ml => new LikerDto
                    {
                        Id = ml.SourceUserId,
                        Username = ml.SourceUser.UserName,
                        KnownAs = ml.SourceUser.KnownAs,
                    })));

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
            
            // Map RegisterDto → AppUser
            CreateMap<RegisterDto, AppUser>();

            // Map AppUser → UserDto
            CreateMap<AppUser, UserDto>();
            //// If you need to map in reverse direction
            //CreateMap<MovieCreateDto, Movie>();
            //CreateMap<MovieUpdateDto, Movie>();
        }
    }
}
