using System;
using FreeWheel.MovieDb.Api.Contexts;
using FreeWheel.MovieDb.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace FreeWheel.MovieDb.Api.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly MoviesContext _db;

        public MoviesService(MoviesContext dbContext)
        {
            _db = dbContext;
        }

        public MoviesContext Context()
        {
            return _db;
        }

        //public void Add(string title, int year, List<Genre> genres)
        //{
        //    var movie = new Movie { Title = title, Year = year, MovieGenres = genres };

        //    movie.MovieId = _db.Movies.Max(m => m.MovieId) + 1;

        //    _db.Movies.Add(movie);

        //    _db.SaveChanges();
        //}

        public IEnumerable<Movie> Find(string title, int year, List<Genre> genres)
        {
            var query = _db.Movies.AsEnumerable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(m => m.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase));
            }

            if (year != 0)
            {
                query = query.Where(m => m.Year == year);
            }

            if (genres.Any())
            {
                //query genres
               genres.ForEach(g =>
               {
                    query = query.Where(m => m.MovieGenres.Any(x => x.GenreId == g.GenreId));
               });
            }

            return query.ToList();

        }

        public IEnumerable<Genre> GetGenres()
        {
           return _db.Genres.ToList();
        }
    }
}