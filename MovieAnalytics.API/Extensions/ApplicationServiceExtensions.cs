using Microsoft.EntityFrameworkCore;
using MovieAnalytics.API.Services;
using MovieAnalytics.API.Services.Interfaces;
using MovieAnalytics.Data;
using MovieAnalytics.Repositories;
using MovieAnalytics.Repositories.Interfaces;
using MovieAnalytics.Services;
using MovieAnalytics.Services.Interfaces;

namespace MovieAnalytics.Extensions
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
            services.AddScoped<IMovieAnalyticsService, MovieAnalyticsService>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;

        }
    }
}
