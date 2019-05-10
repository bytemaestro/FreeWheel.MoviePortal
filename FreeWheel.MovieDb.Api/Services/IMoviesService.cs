using System.Collections.Generic;
using System.Threading.Tasks;
using FreeWheel.MovieDb.Api.Contexts;
using FreeWheel.MovieDb.Api.Models;

namespace FreeWheel.MovieDb.Api.Services
{
    public interface IMoviesService
    {
        MoviesContext Context();

        IEnumerable<Movie> Find(string title, int year, List<Genre> genres);

        IEnumerable<Genre> GetGenres();

        IEnumerable<object> GetAverageMovieRating();

        IEnumerable<object> GetUserRatings(int userId);

        Task<Review> RateMovieAsync(int userId, int movieId, int rating);
    }
}