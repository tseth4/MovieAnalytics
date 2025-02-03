using Microsoft.EntityFrameworkCore;
using MovieAnalytics.API.Data;
using MovieAnalytics.API.Repositories;
using MovieAnalytics.API.Repositories.Interfaces;
using MovieAnalytics.API.Services;
using MovieAnalytics.API.Services.Interfaces;
using MovieAnalytics.Repositories.Interfaces;
using MovieAnalytics.Services;
using MovieAnalytics.Services.Interfaces;

namespace MovieAnalytics.API.Extensions
{
    public static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"))
                    .EnableSensitiveDataLogging() // Include parameters in logs
                    .EnableDetailedErrors();      // Log detailed errors for EF Core

            });
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieImportService, MovieImportService>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IMovieAnalyticsService, MovieAnalyticsService>();
            services.AddScoped<ITokenService, TokenService>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;

        }
    }
}
