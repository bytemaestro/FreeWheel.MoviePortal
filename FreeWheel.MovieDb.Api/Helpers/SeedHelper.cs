using Microsoft.EntityFrameworkCore;
using FreeWheel.MovieDb.Api.Models;

namespace FreeWheel.MovieDb.Api.Helpers
{
    public class SeedHelper
    {
        public static void SeedMovieDb(ModelBuilder modelBuilder)
        {
            #region MovieSeed

            modelBuilder.Entity<Genre>().HasData(
             new Genre { GenreId = 1, Name = "SciFi" },
             new Genre { GenreId = 2, Name = "Fantasy" },
             new Genre { GenreId = 3, Name = "Action" },
             new Genre { GenreId = 4, Name = "Thriller" }
             );

            modelBuilder.Entity<Movie>().HasData(
                 new Movie() { MovieId = 1, Title = "Star Wars - The Empire Strikes Back", Year = 1980 },
                 new Movie() { MovieId = 2, Title = "The Matrix", Year = 1999 },
                 new Movie() { MovieId = 3, Title = "Vanilla Sky", Year = 2001 },
                 new Movie() { MovieId = 4, Title = "Man On Fire", Year = 2004 },
                 new Movie() { MovieId = 5, Title = "Lights Out", Year = 1946 },
                 new Movie() { MovieId = 6, Title = "The Captive", Year = 2014 },
                 new Movie() { MovieId = 7, Title = "Secret in the their Eyes", Year = 1988 },
                 new Movie() { MovieId = 8, Title = "The Butterfly Effect", Year = 2004 },
                 new Movie() { MovieId = 9, Title = "The Sixth Sense", Year = 1999 },
                 new Movie() { MovieId = 10, Title = "National Tresure", Year = 2004 }
                );

            modelBuilder.Entity<Movie>().OwnsMany(p => p.MovieGenres, a =>
            {
                a.HasForeignKey("MovieId");
                a.Property<int>("MovieId");
                a.HasKey("MovieId", "GenreId");
            });

            modelBuilder.Entity<Movie>().OwnsMany(p => p.UserReviews, a =>
            {
                a.HasForeignKey("MovieId");
                a.Property<int>("MovieId");
                a.HasKey("ReviewId", "MovieId", "UserId");
            });

            modelBuilder.Entity<MovieGenre>().HasData(
                new MovieGenre() { MovieId = 1, GenreId = 1 },
                new MovieGenre() { MovieId = 1, GenreId = 2 },
                new MovieGenre() { MovieId = 2, GenreId = 1 },
                new MovieGenre() { MovieId = 2, GenreId = 3 },
                new MovieGenre() { MovieId = 2, GenreId = 4 },
                new MovieGenre() { MovieId = 3, GenreId = 1 },
                new MovieGenre() { MovieId = 3, GenreId = 2 },
                new MovieGenre() { MovieId = 4, GenreId = 3 },
                new MovieGenre() { MovieId = 4, GenreId = 4 },
                new MovieGenre() { MovieId = 5, GenreId = 4 },
                new MovieGenre() { MovieId = 6, GenreId = 4 },
                new MovieGenre() { MovieId = 7, GenreId = 4 },
                new MovieGenre() { MovieId = 8, GenreId = 4 },
                new MovieGenre() { MovieId = 9, GenreId = 4 },
                new MovieGenre() { MovieId = 10, GenreId = 3 },
                new MovieGenre() { MovieId = 10, GenreId = 4 }
                );

            #endregion

            #region UserSeed

            modelBuilder.Entity<User>(u =>
            {
                u.HasData(
                    new User() { UserId = 1, UserName = "reidklein" },
                    new User() { UserId = 2, UserName = "hankaaron" },
                    new User() { UserId = 3, UserName = "johndoe" });

              
            });

            #endregion

            #region ReviewSeed

            modelBuilder.Entity<Review>(r => {
                r.HasData(

                new Review { ReviewId = 1, UserId = 1, MovieId = 1, Rating = 4 },
                new Review { ReviewId = 2, UserId = 1, MovieId = 2, Rating = 5 },
                new Review { ReviewId = 3, UserId = 1, MovieId = 3, Rating = 5 },
                new Review { ReviewId = 4, UserId = 2, MovieId = 1, Rating = 3 },
                new Review { ReviewId = 5, UserId = 2, MovieId = 2, Rating = 4 },
                new Review { ReviewId = 6, UserId = 2, MovieId = 3, Rating = 4 });

               });

            #endregion
        }
    }
}
