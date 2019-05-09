using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeWheel.MovieDb.Api.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public List<string> Genres { get; set; } = new List<string>();

        public List<Review> UserReviews { get; set; } = new List<Review>();

    }
}
