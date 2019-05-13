using Microsoft.EntityFrameworkCore;
using FreeWheel.MovieDb.Api.Models;
using FreeWheel.MovieDb.Api.Helpers;

namespace FreeWheel.MovieDb.Api.Contexts
{
    public class MoviesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Review> Ratings { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
           Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors(true);
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseInMemoryDatabase("Movies");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Relationships

            modelBuilder.Entity<Movie>().OwnsMany(m => m.MovieGenres, mk =>
            {
                mk.HasForeignKey("MovieId");
                mk.Property<int>("MovieId");
                mk.HasKey("MovieId", "GenreId");
            });

            modelBuilder.Entity<Movie>().OwnsMany(m => m.UserReviews, ur =>
            {
                ur.HasForeignKey("MovieId");
                ur.Property<int>("MovieId");
                ur.HasKey("ReviewId", "MovieId", "UserId");
            });

            modelBuilder.Entity<MovieGenre>()
             .HasKey(mg => new { mg.GenreId, mg.MovieId });

            modelBuilder.Entity<Review>()
            .HasKey(mg => new { mg.UserId, mg.MovieId });


            #endregion

            //Seed data
            SeedHelper.SeedMovieDb(modelBuilder);

        }
    }

}