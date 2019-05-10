using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FreeWheel.MovieDb.Api.Models
{
    public class MovieGenre
    {
        [Key]
        public int Id { get; set; }

        public int GenreId { get; set; }
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public Genre Genre { get; set; }
    }
}
