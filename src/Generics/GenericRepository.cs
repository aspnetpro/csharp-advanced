namespace Generics;

public class GenericRepository<T> where T : class, new()
{
    public bool Insert(T entity)
    {
        return true;
    }

    public T GetById(int id)
    {
        return (T)Activator.CreateInstance(typeof(T).GetType());
    }
}
