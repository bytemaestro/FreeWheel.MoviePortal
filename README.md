# FreeWheel.MoviePortal
Movie database with User Ratings
Using dot.net Core 2.2 and Entity Framework Core (Code first - in memory)

Just playing around with .net core 2.2

routes are:

Find:
ex: api/movies/?tile={movieTitle}&year={year}&genreList={Action,Thriller,SciFi,Fantsy only these in any combo}

api/movies/?title=&genreList=Action,Thriller
api/movies/?title=&year=1995
api/movies/?title="Star"

Ratings:
api/movies/ratings/top

ex: api/movies/ratings/topbyuser/{userId}

api/movies/ratings/topbyuser/1

put rating:
ex: api/movies/ratings/rate/{userId}/{movieId}/{rating}

/api/movies/ratings/rate/1/4/3
