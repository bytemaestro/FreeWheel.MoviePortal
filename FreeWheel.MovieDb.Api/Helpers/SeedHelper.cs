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
                 new Movie() { MovieId = 1, Title = "Star Wars - The Empire Strikes Back", Year = 1984 },
                 new Movie() { MovieId = 2, Title = "The Matrix", Year = 1990 },
                 new Movie() { MovieId = 3, Title = "Vanilla Sky", Year = 1995 },
                 new Movie() { MovieId = 4, Title = "Man On Fire", Year = 1995 },
                 new Movie() { MovieId = 5, Title = "Lights Out", Year = 1988 }
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
                a.Property<int>("ReviewId");
                a.HasKey("ReviewId", "MovieId");
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
                new MovieGenre() { MovieId = 5, GenreId = 4 }

                );

        


            #endregion

            #region UserSeed

            modelBuilder.Entity<User>(u =>
            {
                u.HasData(
                    new User() { UserId = 1, UserName = "reidklein" },
                    new User() { UserId = 2, UserName = "hankaaron" },
                    new User() { UserId = 3, UserName = "johndoe" });

               // u.OwnsMany(ur => ur.UserReviews);
            });

            //modelBuilder.Entity<Review>().OwnsOne(p => p.Movie, a =>
            //{
            //    a.HasForeignKey("MovieId");
            //});

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


            modelBuilder.Entity<User>().OwnsMany(p => p.UserReviews, a =>
            {
                a.HasForeignKey("UserId");
                a.Property<int>("ReviewId");
                a.HasKey("ReviewId", "UserId");
            });


     


            #endregion
        }
    }
}
