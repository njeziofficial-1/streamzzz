
namespace StremoCloud.Infrastructure.Data;

public interface IGenericRepository<T>
{
    Task<T> CreateAsync(T entity);
    Task<bool> DeleteAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task<T> GetByEmailAsync(string email);
    Task<bool> UpdateAsync(string id, T entity);
    IQueryable<T> AsQueryable();
}