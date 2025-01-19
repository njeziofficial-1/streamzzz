

namespace StremoCloud.Infrastructure.Data.UnitOfWork;

public interface IStremoUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : class;

}