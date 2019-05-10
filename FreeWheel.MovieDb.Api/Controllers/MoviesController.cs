using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeWheel.MovieDb.Api.Models;
using FreeWheel.MovieDb.Api.Services;

namespace FreeWheel.MovieDb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _movies;

        public MoviesController(IMoviesService service)
        {
            _movies = service;
            _movies.Context().Database.EnsureCreated();
        }

        //// GET: api/Movies
        //[HttpGet]
        //public ActionResult<IEnumerable<Movie>> GetMovies()
        //{
        //    var movies = _movies.Find("", 0 , null);

        //    return movies.ToList();
        //}

        // GET: api/Movies
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetMovies(string title, int year, string genreList)
        {
            if (string.IsNullOrEmpty(title) && year == 0 && string.IsNullOrEmpty(genreList))
            {
                return BadRequest();
            }
            var genres = new List<Genre>();

            if (!string.IsNullOrEmpty(genreList))
            {
                var genresParam = genreList.Split(",").ToList();

                genres = _movies.GetGenres().Where(gl => genresParam.Contains(gl.Name)).ToList();
            }

            var movies = _movies.Find(title, year, genres);

            return movies.ToList();
        }

        //[HttpGet]
        //[Route("api/[controller]/topaverage/")]
        //public ActionResult<IEnumerable<Movie>> GetTopRatedMovies()
        //{
        //}

        //[HttpGet]
        //[Route("api/[controller]/usertop/")]
        //public ActionResult<IEnumerable<Movie>> GetTopRatedMoviesByUser()
        //{
        //}

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

        // PUT: api/Movies/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMovie(int id, Movie movie)
        //{
        //    if (id != movie.MovieId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(movie).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovieExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Movies
        //[HttpPost]
        //public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        //{
        //    _context.Movies.Add(movie);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        //}

        //// DELETE: api/Movies/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Movie>> DeleteMovie(int id)
        //{
        //    var movie = await _context.Movies.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Movies.Remove(movie);
        //    await _context.SaveChangesAsync();

        //    return movie;
        //}

        private bool MovieExists(int id)
        {
            return _movies.Context().Movies.Any(e => e.MovieId == id);
        }
    }
}
