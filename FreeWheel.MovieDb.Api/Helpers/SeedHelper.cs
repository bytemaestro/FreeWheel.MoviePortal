using Microsoft.EntityFrameworkCore;
using FreeWheel.MovieDb.Api.Models;
using System.Collections.Generic;

namespace FreeWheel.MovieDb.Api.Helpers
{
    public class SeedHelper
    {
        public static void SeedMovieDb(ModelBuilder modelBuilder)
        {
            #region MovieSeed

            modelBuilder.Entity<Movie>().HasData(
                new Movie() { MovieId = 1, Title = "Star Wars - The Empire Strikes Back", Year = 1984, Genres = new List<string> { "SciFi", "Fantasy" } },
                new Movie() { MovieId = 2, Title = "The Matrix", Year = 1990, Genres = new List<string> { "SciFi", "Action" } },
                new Movie() { MovieId = 3, Title = "Vanilla Sky", Year = 1995, Genres = new List<string> { "SciFi", "Thriller" } },
                new Movie() { MovieId = 4, Title = "Man On Fire", Year = 1995, Genres = new List<string> { "Thriller", "Action" } });

            #endregion

            #region UserSeed

            modelBuilder.Entity<User>().HasData(
                new User() { UserId = 1, UserName = "reidklein"  },
                new User() { UserId = 2, UserName = "hankaaron" },
                new User() { UserId = 3, UserName = "johndoe" });

            #endregion



            #region ReviewSeed

            modelBuilder.Entity<Review>().HasData(

                new Review { ReviewId = 1, UserId = 1, MovieId = 1 , Rating = 4 },
                new Review { ReviewId = 2, UserId = 1, MovieId = 2, Rating = 5 },
                new Review { ReviewId = 3, UserId = 1, MovieId = 3, Rating = 5 },
                new Review { ReviewId = 4, UserId = 2, MovieId = 1, Rating = 3 },
                new Review { ReviewId = 5, UserId = 2, MovieId = 2, Rating = 4},
                new Review { ReviewId = 6, UserId = 2, MovieId = 3, Rating = 4 }

               );

            #endregion
        }
    }
}
