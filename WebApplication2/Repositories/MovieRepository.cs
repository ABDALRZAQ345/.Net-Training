using Dapper;
using MySqlX.XDevAPI.Common;
using WebApplication2.Database;
using WebApplication2.Models;

namespace WebApplication2.Repositories;

public class MovieRepository : IMovieRepository
{
   private readonly IDbConnectionFactory _dbConnectionFactory;

   public MovieRepository(IDbConnectionFactory dbConnectionFactory)
   {
       _dbConnectionFactory = dbConnectionFactory;
   }

  
   public async Task<bool> CreateAsync(Movie movie)
    {
        
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();
        var result=await connection.ExecuteAsync(new CommandDefinition("""
        insert into movies (id,slug,title,Year)
        values (@Id,@Slug,@Title,@Year)
        """,movie));
        if (result > 0)
        {
            foreach (var genre in movie.Genres)
            {
                await connection.ExecuteAsync(new CommandDefinition("""
                    insert into genres (movieId,name)
                    values (@MovieId,@Name)
                    """,new {MovieId = movie.Id, Name = genre}));
            }
        }
        transaction.Commit();
        return result > 0;


    }

    public async Task<Movie?> GetByIdAsync(Guid id)
    { 
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
     
       var movie= await connection.QuerySingleOrDefaultAsync<Movie>(new CommandDefinition("""
              select * from movies where id = @Id
           """, new {Id = id}));
      if(movie == null) return null;

      var genres = await connection.QueryAsync<string>(
          new CommandDefinition("""
            select name from genres where movieId = @MovieId
          """,new {MovieId = id}));
      foreach (var gen in genres)
      {
          movie.Genres.Add(gen);
      }
      return new Movie()
      {
          Id = movie.Id,
          Title = movie.Title,
          Year = movie.Year,
          Genres=movie.Genres
      };
    }

    public async Task<Movie?> GetBySlugAsync(string slug)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
     
        var movie= await connection.QuerySingleOrDefaultAsync<Movie>(new CommandDefinition("""
               select * from movies where slug = @Slug
            """, new {Slug = slug}));
        if(movie == null) return null;
        var genres = await connection.QueryAsync<string>(
            new CommandDefinition("""
        select name from genres where movieId = @MovieId
        """,new {MovieId = movie.Id}));
        foreach (var gen in genres)
        {
            movie.Genres.Add(gen);
        }
        return new Movie()
        {
            Id = movie.Id,
            Title = movie.Title,
            Year = movie.Year,
            Genres=movie.Genres
        };
        

    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var movies= await connection.QueryAsync(new CommandDefinition("""
        select m.* , string_agg(g.name, ',') as genres
        from movies m left join genres g on m.id = g.movieId
        group by id
        """));
        return movies.Select(x => new Movie()
        {
            Id = x.id,
            Title = x.title,
            Year = x.year,
            Genres= Enumerable.ToList(x.genres.Split(','))
        });
    }

    public  async Task<bool> UpdateAsync(Movie movie)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        throw new NotImplementedException();
       
        
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
   
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(new CommandDefinition("""
                          delete from genres where movieId = @Id 
                     """, new {Id = id}));
        var result=await connection.ExecuteAsync(new CommandDefinition("""
                delete from movies where id = @Id 
                """, new {Id = id}));
        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition("""
            select count(1) from movies where id = @Id                   
         """,new {Id = id}));
      
    }
}