using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParagonID.Data.Repository;

public class Repository : IRepository
{
    private readonly Context.Context _context;

    public Repository(Context.Context context)
    {
        _context = context;
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public T GetById<T>(int id) where T : class
    {
        return _context.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll<T>() where T : class
    {
        return _context.Set<T>().ToList();
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }

    public void Delete<T>(Guid id) where T : class
    {
        var entity = _context.Set<T>().Find(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }

    public IEnumerable<T> Get<T>(Func<T, bool> predicate) where T : class
    {
        return _context.Set<T>().Where(predicate).ToList();
    }
}