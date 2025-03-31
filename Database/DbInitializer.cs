using System.Text;
using Dapper;

namespace WebApplication2.Database;

public class DbInitializer
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DbInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
          
        await connection.ExecuteAsync("""
            create table if not exists movies(id UUID primary key, slug varchar(100),title varchar(100),Year integer not null);
           
        """);
        await connection.ExecuteAsync("""
           
            create table if not exists genres(movieId UUID primary key,name varchar(100));                                                     
           """);

    }
}