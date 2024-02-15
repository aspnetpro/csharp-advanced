using Dapper;
using MyApp.Data;

namespace MyApp.Configuration;

public class Config
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }

    public Config() { }

    public Config(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public T As<T>()
    {
        return this.Value.To<T>();
    }

    public override string ToString()
    {
        return Name;
    }
}

public interface IConfigr
{
    TConfig Get<TConfig>() where TConfig : new();
    void Save<TConfig>(TConfig config);
    Task SaveAsync<TConfig>(TConfig config);
}

public class Configr : IConfigr
{
    private readonly IDbContext _dbContext;
    private readonly IGetConfigsService getConfigsService;

    internal IDictionary<string, Config> GetAllConfigs
    {
        get { return getConfigsService.GetAll(); }
    }

    // Ctor.
    public Configr(IDbContext dbcontext,
        IGetConfigsService getConfigsService)
    {
        _dbContext = dbcontext;
        this.getConfigsService = getConfigsService;
    }

    #region ASYNC

    public async Task SaveAsync<TConfig>(TConfig configuracao)
    {
        var typeConfig = typeof(TConfig);

        var propriedades = from prop in typeConfig.GetProperties()
                           where prop.CanWrite && prop.CanRead
                           where prop.PropertyType.GetTypeConverter().CanConvertFrom(typeof(string))
                           select prop;

        if (!propriedades.Any())
        {
            return;
        }

        var configuracoes = GetAllConfigs;

        foreach (var prop in propriedades)
        {
            var valor = prop.GetValue(configuracao, null) ?? string.Empty;

            string chave = $"{typeConfig.Name}.{prop.Name}".ToLowerInvariant();

            string valorTexto = typeConfig
                .GetTypeConverter()
                .ConvertToInvariantString(valor);

            if (configuracoes != null && configuracoes.ContainsKey(chave))
            {
                await UpdateAsync(configuracoes[chave].Id, valorTexto);
            }
            else
            {
                await InserirAsync(chave, valorTexto);
            }
        }
    }

    private async Task InserirAsync(string name, string value)
    {
        const string sql = @"INSERT INTO dbo.Configr (Name, Value) VALUES (@Name, @Value);";

        using (var conn = _dbContext.CreateConnection())
        {
            await conn.ExecuteAsync(
                sql: sql,
                param: new
                {
                    Nome = name,
                    Valor = value
                });
        }
    }

    private async Task UpdateAsync(int id, string value)
    {
        const string sql = @"UPDATE dbo.Configr SET Value = @Value WHERE Id = @Id";

        using (var conexao = _dbContext.CreateConnection())
        {
            await conexao.ExecuteAsync(
                sql: sql,
                param: new
                {
                    Id = id,
                    Valor = value
                });
        }
    }

    #endregion

    public TConfig Get<TConfig>() where TConfig : new()
    {
        var item = Activator.CreateInstance<TConfig>();

        var typeConfig = typeof(TConfig);

        var properties = from prop in typeof(TConfig).GetProperties()
                           where prop.CanWrite && prop.CanRead
                           let key = $"{typeConfig.Name}.{prop.Name}".ToLowerInvariant()
                           let setting = GetByKey<string>(key)
                           where setting != null
                           where prop.PropertyType.GetTypeConverter().CanConvertFrom(typeof(string))
                           let value = prop.PropertyType.GetTypeConverter().ConvertFromInvariantString(setting)
                           select new
                           {
                               prop,
                               value
                           };

        foreach (var p in properties)
        {
            p.prop.SetValue(item, p.value, null);
        }

        return item;
    }

    public void Save<TConfig>(TConfig config)
    {
        var typeConfig = typeof(TConfig);

        var properties = from prop in typeConfig.GetProperties()
                           where prop.CanWrite && prop.CanRead
                           where prop.PropertyType.GetTypeConverter().CanConvertFrom(typeof(string))
                           select prop;

        if (!properties.Any())
        {
            return;
        }

        IDictionary<string, Config> configs = GetAllConfigs;

        foreach (var prop in properties)
        {
            var value = prop.GetValue(config, null) ?? string.Empty;

            string key = $"{typeConfig.Name}.{prop.Name}".ToLowerInvariant();

            string valorTexto = typeConfig
                .GetTypeConverter()
                .ConvertToInvariantString(value);

            if (configs != null && configs.ContainsKey(key))
            {
                Update(configs[key].Id, valorTexto);
            }
            else
            {
                Insert(key, valorTexto);
            }
        }
    }

    private void Insert(string name, string value)
    {
        const string sql = @"INSERT INTO dbo.Configr (Name, Value) VALUES (@Name, @Value);";
        using (var conn = _dbContext.CreateConnection())
        {
            conn.Execute(
                sql: sql,
                param: new
                {
                    Name = name,
                    Value = value
                });
        }
    }

    private void Update(int id, string value)
    {
        const string sql = @"UPDATE dbo.Configr SET Value = @Value WHERE Id = @Id;";
        using (var conn = _dbContext.CreateConnection())
        {
            conn.Execute(
                sql: sql,
                param: new
                {
                    Id = id,
                    Value = value
                });
        }
    }

    private TType GetByKey<TType>(string chave, TType defaultValue = default(TType))
    {
        if (string.IsNullOrEmpty(chave))
        {
            return defaultValue;
        }

        var config = GetByKey(chave);
        if (config == null)
        {
            return defaultValue;
        }

        return config.As<TType>();
    }

    private Config GetByKey(string key)
    {
        var configs = GetAllConfigs;

        if (!configs.Any())
        {
            return default(Config);
        }

        return configs
            .Where(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase))
            .Select(x => x.Value)
            .FirstOrDefault();
    }
}