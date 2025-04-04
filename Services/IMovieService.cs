﻿using WebApplication2.Models;

namespace WebApplication2.Services;

public interface  IMovieService
{
    Task<bool> CreateAsync (Movie movie);
    
    Task<Movie?> GetByIdAsync (Guid id);
    
    Task<Movie?> GetBySlugAsync (string slug);
    Task<IEnumerable<Movie>> GetAllAsync ();
    
    Task<Movie?> UpdateAsync (Movie movie);
    
    Task<bool> DeleteAsync (Guid id);
    Task<bool> ExistsByIdAsync (Guid id);
     Task<Movie?> GetByIdOrSlugAsync(string idOrSlug);
}