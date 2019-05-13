using System;
using System.ComponentModel.DataAnnotations;

namespace FreeWheel.MovieDb.Api.Models
{
    public class Review
    {
        //private readonly int _maxRating;
        //private int _rating;

        public Review()
        {
            //_maxRating = 5; //todo: get from settings
        }

        public int ReviewId {get; set;}

        public int UserId { get; set; }

        public User User { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }
       
    }
}
