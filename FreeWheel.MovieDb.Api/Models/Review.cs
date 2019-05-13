using System;

namespace FreeWheel.MovieDb.Api.Models
{
    public class Review
    {
        private readonly int _maxRating;
        private int _rating;

        public Review()
        {
            _maxRating = 5; //todo: get from settings
        }

        public int ReviewId {get; set;}

        public int UserId { get; set; }

        public User User { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int Rating
        {
            get => _rating;
            set
            {
                if (value >= 1 && value <= _maxRating)
                {
                    _rating = value;
                }
                else
                {
                    throw new ArgumentException($"Set Rating Error. Rating can only be 1 thru {_maxRating}.");
                }
                
            }
        }
    }
}
