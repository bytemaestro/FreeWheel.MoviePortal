using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeWheel.MovieDb.Api.Models;
using FreeWheel.MovieDb.Api.Services;
using Microsoft.EntityFrameworkCore;
using System;
using FreeWheel.MovieDb.Api.Models.Validation;

namespace FreeWheel.MovieDb.Api.Controllers
{
    [ApiController]
    
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _movies;

        public MoviesController(IMoviesService service)
        {
            _movies = service;
        }

        // GET: api/Movies
        [HttpGet]
        [Route("api/[controller]")]
        public ActionResult<IEnumerable<dynamic>> GetMovies(string title, int year, string genreList)
        {
            if (string.IsNullOrEmpty(title) && year == 0 && string.IsNullOrEmpty(genreList))
            {
                return BadRequest("The movie search criteria is missing or invalid.");
            }

            var genres = new List<Genre>();
            if (!string.IsNullOrEmpty(genreList))
            {
                var genresParam = genreList.Split(",").ToList();
                if (genresParam.Any())
                {
                    genres = _movies.GetGenres().Where(gl => genresParam.Contains(gl.Name)).ToList();
                }
            }

            var movies = _movies.Find(title, year, genres);
            if (movies.Any())
            {
                return movies.ToList();
            }

            return NotFound();
        }

        [HttpGet]
        [Route("api/[controller]/ratings/top")]
        public ActionResult<IEnumerable<object>> GetTopRatedMovies()
        {
            try
            {
                var ratedMovies = _movies.GetMoviesWithAverageRating().Take(5).ToList();
                if (ratedMovies != null && ratedMovies.Any())
                {
                    return ratedMovies;
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
           
        }

        [HttpGet]
        [Route("api/[controller]/ratings/topbyuser/{userId}")]
        public ActionResult<IEnumerable<object>> GetTopRatedMoviesByUser(int userId)
        {
            try
            {
                var ratedMovies = _movies.GetUserRatings(userId).Take(5).ToList();
                if (ratedMovies != null && ratedMovies.Any())
                {
                    return ratedMovies;
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _movies.Context().Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [ValidateModel]
        [HttpPut]
        [Route("api/[controller]/ratings/rate/{userId}/{movieId}/{rating}")]
        public async Task<ActionResult<Review>> PutRating(int userId, int movieId, [Bind("Review,Rating, Review.Rating")]int rating)
        {
            if (userId == 0 || movieId == 0 )
            {
                return NoContent();
            }

            try
            {
                //TODO: !! Want to use Model Binding/Validation running out of time!!
                if (rating < 0 || rating > 5)
                {
                    return ValidationProblem(new ValidationProblemDetails()
                    {
                        Title = "Validation Error",
                        Detail = "Rating must be from 1 to 5",
                        Status = 400
                    });
                }

                if (ModelState.IsValid)
                {
                    var response = await _movies.RateMovieAsync(userId, movieId, rating).ConfigureAwait(false);
                    if (response != null)
                    {
                        return new OkResult();
                    }
                }

                return BadRequest();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(userId, movieId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            catch (ArgumentException)
            {

                return ValidationProblem();
            }
        }

        private bool MovieExists(int id)
        {
            return _movies.Context().Movies.Any(e => e.MovieId == id);
        }

        private bool RatingExists(int userId, int movieId)
        {
            return _movies.Context().Ratings.Any(e => e.User.UserId == userId);
        }
    }
}
