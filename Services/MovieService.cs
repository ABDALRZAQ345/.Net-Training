using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Services;

public class MovieService: IMovieService
{
    private readonly MovieRepository _movieRepository;

    public MovieService (MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<bool> CreateAsync(Movie movie)
    {
       return await _movieRepository.CreateAsync(movie);
    }

    public async Task<Movie?> GetByIdAsync(Guid id)
    {
        return await _movieRepository.GetByIdAsync(id);
    }

    public async Task<Movie?> GetByIdOrSlugAsync(string idOrSlug)
    {
        var movie= Guid.TryParse(idOrSlug, out Guid id) ?
            await _movieRepository.GetByIdAsync(id) :
            await _movieRepository.GetBySlugAsync(idOrSlug);
        
        return movie;
    }

    public  async Task<Movie?> GetBySlugAsync(string slug)
    {
         return await _movieRepository.GetBySlugAsync(slug);
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _movieRepository.GetAllAsync();
    }

    public async Task<Movie?> UpdateAsync(Movie movie)
    {
       var existing = await _movieRepository.ExistsByIdAsync(movie.Id);
        if(!existing) return null;
         await _movieRepository.UpdateAsync(movie);
        return movie;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
      return await _movieRepository.DeleteAsync(id);
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
       return await _movieRepository.ExistsByIdAsync(id);
    }
}