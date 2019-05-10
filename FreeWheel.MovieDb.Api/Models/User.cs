using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreeWheel.MovieDb.Api.Models
{
    public class User 
    {
        [Key]
        public int UserId { get; set; }

        public string UserName {get; set;}

        public virtual ICollection<Review> UserReviews { get; } = new List<Review>();

    }
}
