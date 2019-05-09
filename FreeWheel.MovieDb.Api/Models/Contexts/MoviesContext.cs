using Microsoft.EntityFrameworkCore;
using FreeWheel.MovieDb.Api.Models;
using FreeWheel.MovieDb.Api.Helpers;

namespace FreeWheel.MovieDb.Api.Contexts
{
    public class MoviesContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Ratings { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
            Database.EnsureCreated(); //seems necessary for In-Memory creation
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors(true);
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseInMemoryDatabase("Movies");
            //optionsBuilder.UseInMemoryDatabase("server=.;database=moviesDb;trusted_connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Relationships

            //Seed data
            SeedHelper.SeedMovieDb(modelBuilder);

            modelBuilder.Entity<Review>()
            .HasKey(r => new { r.MovieId, r.UserId });

            modelBuilder.Entity<Review>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserReviews)
                .HasForeignKey(ur => ur.ReviewId);

            modelBuilder.Entity<Review>()
              .HasOne(ur => ur.Movie)
              .WithMany(m => m.UserReviews)
              .HasForeignKey(ur => ur.ReviewId);

            #endregion

          

        }
    }

}