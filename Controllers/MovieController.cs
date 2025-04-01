using Microsoft.AspNetCore.Mvc;
using WebApplication2.ApiEndPoints;
using WebApplication2.Mapping;
using WebApplication2.Models;
using WebApplication2.Repositories;
using WebApplication2.Requests;
using WebApplication2.Services;

namespace WebApplication2.Controllers;

[ApiController]

public class MovieController : ControllerBase
{
    private readonly MovieRepository _movieRepository;
    private readonly MovieService _movieService;
    public MovieController(MovieRepository movieRepository, MovieService movieService)
    {
        _movieRepository = movieRepository;
        _movieService = movieService;
    }

   

    [HttpGet(Api.Movies.GetAll)]
    public  async Task<IActionResult>  Index()
    {
      
       var movies = await _movieService.GetAllAsync();
        
            return Ok(movies);
    }

    [HttpGet(Api.Movies.Get)]
    public async Task<IActionResult> Get([FromRoute]string  idOrSlug)
    {
     
      var movie=await _movieService.GetByIdOrSlugAsync(idOrSlug);
        if(movie is null) return NotFound();
      return Ok( movie.MapToMovieResponse());
        
    }
    [HttpPut(Api.Movies.Update)]
    public async Task<IActionResult> Update([FromRoute]Guid id ,[FromBody]UpdateMovieRequest request)
    {
       
       var movie = request.MapToMovie(id);
       var updatedMovie =await _movieService.UpdateAsync(movie);
       if(updatedMovie != null)
           return NotFound();
       return Ok(movie.MapToMovieResponse());
    }

    [HttpPost(Api.Movies.Create)]
    public async Task<IActionResult> Create([FromBody]CreateMovieRequest request)
    {
        var movie = request.MapToMovie();
       await _movieService.CreateAsync(movie);
       return Created(Api.Movies.Create+"/"+movie.Id, movie);
    }
    
    [HttpDelete(Api.Movies.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
         bool status=await _movieService.DeleteAsync(id);
         if(!status) return NotFound();
         return NoContent();
    }
    

}