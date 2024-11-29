using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
namespace StremoCloud.Infrastructure.Data;

public class DataContext(IMongoDatabase database) : IDataContext
{
    public IMongoCollection<T> GetCollection<T>(string name)
        => database.GetCollection<T>(name);
}
