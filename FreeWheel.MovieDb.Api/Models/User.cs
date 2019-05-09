using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeWheel.MovieDb.Api.Models
{
    [Table("User")]
    public class User 
    {
        [Key]
        public int UserId { get; set; }

        public string UserName {get; set;}

        public List<Review> UserReviews { get; set; }

    }
}
