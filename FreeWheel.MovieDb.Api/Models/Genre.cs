using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreeWheel.MovieDb.Api.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres { get; } = new List<MovieGenre>();

    }
}
