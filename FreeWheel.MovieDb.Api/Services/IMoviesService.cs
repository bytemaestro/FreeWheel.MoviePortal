using System.Collections.Generic;
using FreeWheel.MovieDb.Api.Contexts;
using FreeWheel.MovieDb.Api.Models;

namespace FreeWheel.MovieDb.Api.Services
{
    public interface IMoviesService
    {
        MoviesContext Context();

       // void Add(string title, int year, List<MovieGenre> genres);

        IEnumerable<Movie> Find(string title, int year, List<Genre> genres);

        IEnumerable<Genre> GetGenres();
    }
}