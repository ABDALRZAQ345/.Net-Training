using Dapper;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using WebApplication2.Database;
using WebApplication2.Models;

namespace WebApplication2.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> CreateAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Movie?> GetByIdAsync(Guid id)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        return movie;
    }

    public async Task<Movie?> GetBySlugAsync(string slug)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.slug == slug);
        return movie;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        var movies = await _context.Movies.ToListAsync();
        return movies;
    }

    public async Task<bool> UpdateAsync(Movie movie)
    {
        var existingMovie = await _context.Movies.FindAsync(movie.Id);
        if (existingMovie == null) return false; 

        _context.Entry(existingMovie).CurrentValues.SetValues(movie);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return false;
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await _context.Movies.AnyAsync(m => m.Id == id);
    }
}