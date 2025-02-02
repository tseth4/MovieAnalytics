using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieAnalytics.API.Entities;

namespace MovieAnalytics.API.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
    {

        private static readonly ILoggerFactory DbLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ProductionCompany> ProductionCompanies { get; set; }
        public DbSet<Language> Languages { get; set; }

        // Junction table DbSets
        public DbSet<MovieDirector> MovieDirectors { get; set; }
        public DbSet<MovieWriter> MovieWriters { get; set; }
        public DbSet<MovieStar> MovieStars { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieCountry> MovieCountries { get; set; }
        public DbSet<MovieProductionCompany> MovieProductionCompanies { get; set; }
        public DbSet<MovieLanguage> MovieLanguages { get; set; }
        // Add other DbSets

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseLoggerFactory(DbLoggerFactory)       // Enable console logging for EF Core
                .EnableSensitiveDataLogging()           // Log query parameters (only for debugging)
                .EnableDetailedErrors();                // Log detailed EF Core errors
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Budget).HasColumnType("NUMERIC"); // Explicitly map to NUMERIC
                entity.Property(e => e.GrossWorldWide).HasColumnType("NUMERIC");
                entity.Property(e => e.GrossUsCanada).HasColumnType("NUMERIC");
                entity.Property(e => e.OpeningWeekendGross).HasColumnType("NUMERIC");
                entity.Property(e => e.Rating).HasColumnType("NUMERIC");
            });

            // Movie director
            // Configure many-to-many relationships
            // Configure Composite primary key
            modelBuilder.Entity<MovieDirector>()
                .HasKey(md => new { md.MovieId, md.DirectorId });


            modelBuilder.Entity<MovieDirector>()
                .HasOne(md => md.Movie) // A MovieDirector is related to one Movie
                .WithMany(m => m.MovieDirectors) // A Movie can have many MovieDirectors
                .HasForeignKey(md => md.MovieId); // MovieId is the foreign key

            modelBuilder.Entity<MovieDirector>()
                .HasOne(md => md.Director)
                .WithMany(d => d.MovieDirectors)
                .HasForeignKey(md => md.DirectorId);

            // MovieWriter
            modelBuilder.Entity<MovieWriter>()
                .HasKey(mw => new { mw.MovieId, mw.WriterId });

            modelBuilder.Entity<MovieWriter>()
                .HasOne(mw => mw.Movie)
                .WithMany(m => m.MovieWriters)
                .HasForeignKey(mw => mw.MovieId);

            modelBuilder.Entity<MovieWriter>()
                .HasOne(mw => mw.Writer)
                .WithMany(w => w.MovieWriters)
                .HasForeignKey(mw => mw.WriterId);

            // MovieStar
            modelBuilder.Entity<MovieStar>()
                .HasKey(ms => new { ms.MovieId, ms.StarId });

            modelBuilder.Entity<MovieStar>()
                .HasOne(ms => ms.Movie)
                .WithMany(m => m.MovieStars)
                .HasForeignKey(ms => ms.MovieId);

            modelBuilder.Entity<MovieStar>()
                .HasOne(ms => ms.Star)
                .WithMany(s => s.MovieStars)
                .HasForeignKey(ms => ms.StarId);

            // MovieGenre
            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

            // MovieCountry
            modelBuilder.Entity<MovieCountry>()
                .HasKey(mc => new { mc.MovieId, mc.CountryId });

            modelBuilder.Entity<MovieCountry>()
                .HasOne(mc => mc.Movie)
                .WithMany(m => m.MovieCountries)
                .HasForeignKey(mc => mc.MovieId);

            modelBuilder.Entity<MovieCountry>()
                .HasOne(mc => mc.Country)
                .WithMany(c => c.MovieCountries)
                .HasForeignKey(mc => mc.CountryId);

            // MovieProductionCompany
            modelBuilder.Entity<MovieProductionCompany>()
                .HasKey(mpc => new { mpc.MovieId, mpc.ProductionCompanyId });

            modelBuilder.Entity<MovieProductionCompany>()
                .HasOne(mpc => mpc.Movie)
                .WithMany(m => m.MovieProductionCompanies)
                .HasForeignKey(mpc => mpc.MovieId);

            modelBuilder.Entity<MovieProductionCompany>()
                .HasOne(mpc => mpc.ProductionCompany)
                .WithMany(pc => pc.MovieProductionCompanies)
                .HasForeignKey(mpc => mpc.ProductionCompanyId);

            // MovieLanguage
            modelBuilder.Entity<MovieLanguage>()
                .HasKey(ml => new { ml.MovieId, ml.LanguageId });

            modelBuilder.Entity<MovieLanguage>()
                .HasOne(ml => ml.Movie)
                .WithMany(m => m.MovieLanguages)
                .HasForeignKey(ml => ml.MovieId);

            modelBuilder.Entity<MovieLanguage>()
                .HasOne(ml => ml.Language)
                .WithMany(l => l.MovieLanguages)
                .HasForeignKey(ml => ml.LanguageId);

        }
    }
}
