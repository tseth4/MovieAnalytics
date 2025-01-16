using Microsoft.EntityFrameworkCore;
using MovieAnalytics.Data;
using MovieAnalytics.Services.Interfaces;
using MovieAnalytics.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IMovieRepository, MovieRepository>();
//builder.Services.AddScoped<IMovieImportService, MovieImportService>();


//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var movieImportService = services.GetRequiredService<IMovieImportService>();

    // Ensure database is created/migrated
    await context.Database.MigrateAsync();

    // Check if database is empty before importing
    if (!context.Movies.Any())
    {
        try
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "movies.csv");
            await movieImportService.ImportMoviesFromCsv(filePath);
            Console.WriteLine("Data import completed successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during data import: {ex.Message}");
        }
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
