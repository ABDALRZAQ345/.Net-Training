using WebApplication2.Models;
using WebApplication2.Requests;
using WebApplication2.Responses;

namespace WebApplication2.Mapping;

public static class ContractMapping
{
    public static Movie MapToMovie(this CreateMovieRequest request)
    {
      return  new Movie
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Year = request.Year,
            Genres = request.Genres.ToList(),

        }; 
    }
    public static Movie MapToMovie(this UpdateMovieRequest request,Guid Id)
    {
        return  new Movie
        {
            Id = Id,
            Title = request.Title,
            Year = request.Year,
            Genres = request.Genres.ToList(),

        }; 
    }


    public static MovieResponse MapToMovieResponse(this Movie movie)
    {
        return new MovieResponse
        {
            id = movie.Id,
            Title = movie.Title,
            Year = movie.Year,
            Genres = movie.Genres.ToList(),
            slug= movie.slug,
        };
    }

    public static MoviesResponse MapToMoviesResponse(this IEnumerable<Movie> movies)
    {
        return new MoviesResponse()
        {
            movies = movies.Select(MapToMovieResponse),
        };
    }
    
}