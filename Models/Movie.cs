using System.Text.RegularExpressions;

namespace WebApplication2.Models;

public class Movie
{
     public required Guid Id { get; init; }
     public required string Title { get; init; }
     public required int Year { get; init; }
     public required List<string> Genres { get; init; } = new List<string>();
     public string slug => generateSlug();
     

     private string generateSlug()
     {
          var slugged=Regex.Replace(Title,"[^0-9A-Za-z _-]",string.Empty)
               .ToLower().Replace(" ","-");
          return slugged+Year;
     }


}