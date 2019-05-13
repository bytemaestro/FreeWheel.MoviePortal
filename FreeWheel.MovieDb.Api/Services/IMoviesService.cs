using System.Collections.Generic;
using System.Threading.Tasks;
using FreeWheel.MovieDb.Api.Contexts;
using FreeWheel.MovieDb.Api.Models;

namespace FreeWheel.MovieDb.Api.Services
{
    public interface IMoviesService
    {
        MoviesContext Context();

        IEnumerable<dynamic> Find(string title, int year, List<Genre> genres);

        IEnumerable<Genre> GetGenres();

        IEnumerable<dynamic> GetMoviesWithAverageRating();

        IEnumerable<dynamic> GetUserRatings(int userId);

        Task<Review> RateMovieAsync(int userId, int movieId, int rating);
    }
}