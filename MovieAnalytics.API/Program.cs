using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieAnalytics.API.Data;
using MovieAnalytics.API.Entities;
using MovieAnalytics.API.Extensions;
using MovieAnalytics.Extensions;
using MovieAnalytics.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:5173"); // Your Vite React app's default port
    });
});

var app = builder.Build();

// Serve static files from wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseRouting();

// ✅ Handles Bearer tokens
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

// app.MapFallbackToFile("index.html");
app.MapFallbackToFile("index.html");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var movieImportService = services.GetRequiredService<IMovieImportService>();

    try
    {
        Console.WriteLine("Applying migrations...");
        await context.Database.MigrateAsync();
        Console.WriteLine("Migrations applied successfully.");

        // if (!context.Movies.Any())
        // {
        //     Console.WriteLine("Seeding database...");
        //     string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "movies.csv");
        //     await movieImportService.ImportMoviesFromCsv(filePath);
        //     Console.WriteLine("Database seeding completed successfully.");
        // }
        // else
        // {
        //     Console.WriteLine("Database already contains data. Skipping seeding.");
        // }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during migration or seeding: {ex.Message}");
    }
}

app.Run();
