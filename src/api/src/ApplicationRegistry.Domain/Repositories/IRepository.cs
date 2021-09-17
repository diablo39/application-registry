using System;

namespace ApplicationRegistry.Domain.Persistency
{
    public interface IRepository<T>
        where T: class
    {
        bool Exists(Func<T, bool> predicate);

        bool Exists<K>(Func<K, bool> predicate)
            where K : class, T;

        void Add(T entity);

        T Get(params object[] keyValues);
    }
}
