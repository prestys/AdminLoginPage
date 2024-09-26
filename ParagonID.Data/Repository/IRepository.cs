public interface IRepository
{
    void Add<T>(T entity) where T : class;
    T GetById<T>(int id) where T : class;
    IEnumerable<T> GetAll<T>() where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(Guid id) where T : class;
    IEnumerable<T> Get<T>(Func<T, bool> predicate) where T : class;
}