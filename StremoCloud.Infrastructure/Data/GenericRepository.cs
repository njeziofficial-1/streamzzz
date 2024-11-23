using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace StremoCloud.Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T>
{
    private readonly IMongoCollection<T> _collection;

    public GenericRepository(IDataContext context)
    {
        _collection = context.GetCollection<T>(typeof(T).Name);
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var response = await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
        return response.IsAcknowledged && response.DeletedCount > 0;

    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(x => true).ToListAsync();
    }

    public async Task<bool> UpdateAsync(string id, T entity)
    {
        var response = await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);
        return response.IsAcknowledged && response.ModifiedCount > 0;
    }

    public async Task<T> GetByEmailAsync(string email)
    {
        return await _collection.Find(Builders<T>.Filter.Eq("Email", email)).FirstOrDefaultAsync();
    }
}
