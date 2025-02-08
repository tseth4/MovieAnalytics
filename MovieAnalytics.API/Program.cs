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

builder.Services.AddCors();


var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
  .WithOrigins("https://localhost:5173", "http://localhost:5173"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Serve static files from wwwroot
// ORDER MATTERS
// OUTPUTING FILES
app.UseDefaultFiles();
// ALLOW API to serve static js files
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


// app.MapFallbackToFile("index.html");
app.MapFallbackToController("Index", "Fallback");



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
