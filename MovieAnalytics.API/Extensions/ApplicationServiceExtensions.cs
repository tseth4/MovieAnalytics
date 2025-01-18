using MovieAnalytics.Repositories.Interfaces;
using MovieAnalytics.Repositories;
using MovieAnalytics.Services.Interfaces;
using MovieAnalytics.Services;
using MovieAnalytics.Data;
using Microsoft.EntityFrameworkCore;
using MovieAnalytics.API.Services.Interfaces;
using MovieAnalytics.API.Services;

namespace MovieAnalytics.Extensions
{
    public static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));

            });
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieImportService, MovieImportService>();
            services.AddScoped<IMovieAnalyticsService, MovieAnalyticsService>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;

        }
    }
}
