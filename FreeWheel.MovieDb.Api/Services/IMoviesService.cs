using System.Collections.Generic;
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

        void RateMovie(int userId, int movieId, int rating);
        IEnumerable<object> GetUserRatings(int userId);
    }
}