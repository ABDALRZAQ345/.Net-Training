using Microsoft.AspNetCore.Mvc;
using WebApplication2.ApiEndPoints;
using WebApplication2.Mapping;
using WebApplication2.Models;
using WebApplication2.Repositories;
using WebApplication2.Requests;

namespace WebApplication2.Controllers;

[ApiController]

public class MovieController : ControllerBase
{
    private readonly MovieRepository _movieRepository;

    public MovieController(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

   

    [HttpGet(Api.Movies.GetAll)]
    public  async Task<IActionResult>  Index()
    {
        
       var movies =await _movieRepository.GetAllAsync();
        
            return Ok(movies);
    }

    [HttpGet(Api.Movies.Get)]
    public async Task<IActionResult> Get([FromRoute]string  idOrSlug)
    {
     
      var movie= Guid.TryParse(idOrSlug, out Guid id) ?
              await _movieRepository.GetByIdAsync(id) :
               await _movieRepository.GetBySlugAsync(idOrSlug);
      
   
        if (movie == null)
            return NotFound();
        
        return Ok( movie.MapToMovieResponse());
        
    }
    [HttpPut(Api.Movies.Update)]
    public async Task<IActionResult> Update([FromRoute]Guid id ,[FromBody]UpdateMovieRequest request)
    {
        var movie =await _movieRepository.GetByIdAsync(id);
        if (movie == null)
                return NotFound();
         
        movie = request.MapToMovie(id);
        
       bool updated= await _movieRepository.UpdateAsync( movie);
       if(!updated)
           return NotFound();
       return Ok(movie.MapToMovieResponse());
    }

    [HttpPost(Api.Movies.Create)]
    public async Task<IActionResult> Create([FromBody]CreateMovieRequest request)
    {
        var movie = request.MapToMovie();
       await _movieRepository.CreateAsync(movie);
       return Created(Api.Movies.Create+"/"+movie.Id, movie);
    }
    
    [HttpDelete(Api.Movies.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
         bool status=await _movieRepository.DeleteAsync(id);
         if(!status) return NotFound();
         return NoContent();
    }
    

}