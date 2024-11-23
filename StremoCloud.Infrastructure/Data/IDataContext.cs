using MongoDB.Driver;

namespace StremoCloud.Infrastructure.Data
{
    public interface IDataContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}