using Microsoft.EntityFrameworkCore;
using FreeWheel.MovieDb.Api.Models;

namespace FreeWheel.MovieDb.Api.Data
{
    public class MovieDb : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Relationships

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