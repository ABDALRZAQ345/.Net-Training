using System.Data;
using FluentValidation;
using WebApplication2.Models;

namespace WebApplication2.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
     public MovieValidator()
     {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Year).NotEmpty().WithMessage("Year is required"); 
            RuleFor(x=>x.Year).LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("Year must be greater than or equal to DateTime.UtcNow.Year");
            RuleFor(x=> x.Genres).NotEmpty().WithMessage("Genre is required");
            //? example for specific rule 
            RuleFor(x=> x.Title).MustAsync(ValidateTile).WithMessage("Title is too long");
     }

     private async Task<bool> ValidateTile(Movie movie,string title,CancellationToken cancellationToken)
     {
      return   title.Length <= 50;
     }
}