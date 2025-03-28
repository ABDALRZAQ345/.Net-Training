namespace WebApplication2.Responses;

public class MoviesResponse
{
    public required IEnumerable<MovieResponse> movies { get; init; } = Enumerable.Empty<MovieResponse>();
    
}