using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagonID.Data.Service;

public class DataRetriever
{
    private readonly IRepository _repository;

    public DataRetriever(IRepository repository)
    {
        _repository = repository;
    }

    public void Add<T>(T entity) where T : class
    {
        _repository.Add(entity);
    }

    public T GetById<T>(int id) where T : class
    {
        return _repository.GetById<T>(id);
    }

    public IEnumerable<T> GetAll<T>() where T : class
    {
        return _repository.GetAll<T>();
    }

    public void Update<T>(T entity) where T : class
    {
        _repository.Update(entity);
    }

    public void Delete<T>(Guid id) where T : class
    {
        _repository.Delete<T>(id);
    }

    public IEnumerable<T> Get<T>(Func<T, bool> predicate) where T : class
    {
        return _repository.Get(predicate);
    }
}