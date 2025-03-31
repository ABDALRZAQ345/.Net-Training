namespace WebApplication2.Responses;

public class MovieResponse
{
   public  required Guid   id { get; init; }
    public required string Title { get; init; }
    public required int Year { get; init; }
    public string slug { get; init; }
    public required IEnumerable<string> Genres { get; init; }=Enumerable.Empty<string>();

}