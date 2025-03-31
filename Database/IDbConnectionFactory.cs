using System.Data;
using MySql.Data.MySqlClient;
using Npgsql;

namespace WebApplication2.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}

public class MysqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public MysqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}

public class NpgsqlConnectionFactory : IDbConnectionFactory 
{
    private readonly string _connectionString;

    public NpgsqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
       await connection.OpenAsync();
        return connection;
        
    }
}