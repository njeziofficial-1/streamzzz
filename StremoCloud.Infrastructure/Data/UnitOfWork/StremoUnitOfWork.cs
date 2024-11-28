using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Infrastructure.Data.UnitOfWork;

public class StremoUnitOfWork : IStremoUnitOfWork
{
    DataContext _dataContext;

    public StremoUnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    readonly Dictionary<Type, object> _repositories = [];
    public Dictionary<Type, object> Repositories
    {
        get { return _repositories; }
        set { Repositories = value; }
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (Repositories.ContainsKey(typeof(T)))
        {
            return Repositories[typeof(T)] as IGenericRepository<T>;
        }

        IGenericRepository<T> repo = new GenericRepository<T>(_dataContext);
        Repositories.Add(typeof(T), repo);
        return repo;
    }
}
