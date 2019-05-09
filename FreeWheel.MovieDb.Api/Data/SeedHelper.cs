using Microsoft.EntityFrameworkCore;
using FreeWheel.MovieDb.Api.Models;

namespace FreeWheel.MovieDb.Api.Data
{
    public class SeedHelper
    {
        public void SeedMovieDb(ModelBuilder modelBuilder)
        {
            #region MovieSeed

            modelBuilder.Entity<Movie>().HasData(
                new Movie() { MovieId = 1, Title = "Star Wars - The Empire Strikes Back", Year = 1984 },
                new Movie() { MovieId = 2, Title = "The Matrix", Year = 1990 },
                new Movie() { MovieId = 3, Title = "Vanilla Sky", Year = 1995 });

            #endregion

            #region UserSeed

            modelBuilder.Entity<User>().HasData(
                new User() { UserId = 1, UserName = "reidklein"  },
                new User() { UserId = 2, UserName = "hankaaron" },
                new User() { UserId = 2, UserName = "johndoe" });

            #endregion



            //#region ReviewSeed

            //modelBuilder.Entity<Review>().OwnsOne(p => p.AuthorName).HasData(

            //    new { PostId = 1, First = "Andriy", Last = "Svyryd" },

            //    new { PostId = 2, First = "Diego", Last = "Vega" });

            //#endregion
        }
    }
}
