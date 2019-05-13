using System;
using FreeWheel.MovieDb.Api.Contexts;
using FreeWheel.MovieDb.Api.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        /// <summary>
        /// Rates a movie by userId, movieId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="movieId"></param>
        /// <param name="rating"></param>
        public async Task<Review> RateMovieAsync(int userId, int movieId, int rating)
        {
            try
            {
                var movie = _db.Movies.Where(m => m.MovieId == movieId).FirstOrDefault();
                if (movie == null)
                {
                    throw new ArgumentException("Movie not found to rate!");
                }

                var review = movie.UserReviews.Where(ur => ur.UserId == userId).FirstOrDefault();
                if (review != null)
                {
                     review.Rating = rating;

                    _db.Entry(review).State = EntityState.Modified;

                    _db.Ratings.Update(review);

                }
                else
                {
                    var reviewId = _db.Ratings.Max(x => x.ReviewId); 
                    review = new Review { ReviewId = reviewId + 1, MovieId = movieId, UserId = userId, Rating = rating };

                    _db.Entry(review).State = EntityState.Added;

                    _db.Ratings.Add(review);
                }

                await _db.SaveChangesAsync().ConfigureAwait(false);

                return review;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Finds moves by title, year, or genre.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="year"></param>
        /// <param name="genres"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> Find(string title, int year, List<Genre> genres)
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
                //query only matching all genres
                genres.ForEach(g =>
                {
                    query = query.Where(m => m.MovieGenres.Any(x => x.GenreId == g.GenreId));
                });
            }

           return query.Select(m =>
           {
               return new
               {
                   m.MovieId,
                   m.Title,
                   YearOfRelease = m.Year,
                   m.RunningTime,
                   Genres = string.Join(",", m.MovieGenres.Select(mg => mg.Genre.Name)),
                   AverageRating = GetAverageMovieRating(m.MovieId)

               };
           }).ToList();
        }

        public dynamic GetAverageMovieRating(int movieId)
        {
            var averageRating = 0d;

            var movie = _db.Movies.Find(movieId);
            if (movie != null)
            {
                if (movie.UserReviews.Any())
                {
                    averageRating = movie.UserReviews.Where(ur => ur != null).Average(ur => ur.Rating);
                }
            }
            
            return averageRating;
        }

        /// <summary>
        /// Returns the top average ratings
        /// </summary>
        /// <returns>List of Movie, and average use rating.</returns>
        public IEnumerable<object> GetMoviesWithAverageRating()
        {
            List<dynamic> ratings = new List<dynamic>();

           _db.Movies.ToList().ForEach(m =>
            {
                var rates = m.UserReviews
                      .Where(r => r != null)
                      .GroupBy(r => r.Movie.Title,  rt => rt.Rating)
                      .Select(g => new
                      {
                          MovieId = g.Key,
                          AverageRating = g.Average()
                      });

                if (rates.Any())
                {
                    ratings.AddRange(rates);
                }
                    
           });

          return ratings.OrderByDescending(r => r.AverageRating).ToList();
        }

        public IEnumerable<object> GetUserRatings(int userId)
        {
          List<dynamic> ratings = new List<dynamic>();

          _db.Movies.ToList().ForEach(mr =>
          {
              mr.UserReviews.Where(ur => ur.UserId == userId).ToList().ForEach ( r=>
              {
                  ratings.Add(new { r.MovieId, r.Movie.Title, r.Movie.Year, r.Rating });
              });
          });

            return ratings.OrderBy(x => x.Title).OrderByDescending(x => x.Rating);
        }
        
        /// <summary>
        /// GetGenres returns the system movie genres.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Genre> GetGenres()
        {
           return _db.Genres.ToList();
        }
    }
}