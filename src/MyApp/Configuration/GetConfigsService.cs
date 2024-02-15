using Dapper;
using MyApp.Data;

namespace MyApp.Configuration;

public interface IGetConfigsService
{
    IDictionary<string, Config> GetAll();
}

public class GetConfigsService : IGetConfigsService
{
    private readonly IDbContext dbContext;

    public GetConfigsService(IDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IDictionary<string, Config> GetAll()
    {
        using (var conexao = this.dbContext.CreateConnection())
        {
            return conexao
                .Query<Config>(sql: "SELECT Id, Name, Value FROM dbo.Configr")
                .ToDictionary(s => s.Name.ToLowerInvariant());
        }
    }
}
