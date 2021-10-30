using ApplicationRegistry.Database;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        void UpdateChildCollection<TChild, TKey>(T entity, Expression<Func<T, IEnumerable<TChild>>> property, IEnumerable<TChild> modifiedCollection, Action<TChild, TChild> updater)
            where TChild : IEntity<TKey>;
    }
}
