using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
namespace StremoCloud.Infrastructure.Data;

public class DataContext : IDataContext
{
    private readonly IMongoDatabase _database;
    public DataContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDbOptions:ConnectionString"]);
        _database = client.GetDatabase(configuration["MongoDbOptions:DatabaseName"]);

    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }

}
