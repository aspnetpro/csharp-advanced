using Microsoft.Data.SqlClient;
using System.Data;

namespace MyApp.Data;

public interface IDbContext
{
    IDbConnection CreateConnection();
}

public class MSSqlDbContext : IDbContext
{
    private readonly string _connectionString;

    // Default ctor.
    public MSSqlDbContext(string connString)
    {
        _connectionString = connString;
    }

    public IDbConnection CreateConnection() => 
        new SqlConnection(_connectionString);
}
