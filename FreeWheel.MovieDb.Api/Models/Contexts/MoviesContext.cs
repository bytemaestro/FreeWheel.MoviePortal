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
            modelBuilder.Entity<MovieGenre>()
             .HasKey(t => new { t.GenreId, t.MovieId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(m => m.Movie)
                .WithMany(mg => mg.MovieGenres)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<Review>()
            .HasKey(r => new { r.MovieId, r.UserId });

            modelBuilder.Entity<Review>()
                .HasOne(ur => ur.Movie)
                .WithMany(m => m.UserReviews)
                .HasForeignKey(ur => ur.MovieId);

            modelBuilder.Entity<Review>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserReviews)
                .HasForeignKey(ur => ur.UserId);

            


            #endregion

            //Seed data
            SeedHelper.SeedMovieDb(modelBuilder);


        }
    }

}