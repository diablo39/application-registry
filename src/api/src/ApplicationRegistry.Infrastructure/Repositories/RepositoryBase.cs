using ApplicationRegistry.Database;
using ApplicationRegistry.Domain.Persistency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Infrastructure.Domain.Persistency
{
    internal class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _set;

        public RepositoryBase(DbContext context)
        {
            _set = context.Set<T>();
            _context = context;
        }

        public virtual void Add(T entity)
        {
            _set.Add(entity);
        }

        public virtual bool Exists(Func<T, bool> predicate)
        {
            return _set.Local.Any(predicate) || _set.Any(predicate);
        }

        public virtual bool Exists<K>(Func<K, bool> predicate)
            where K : class, T
        {
            return _set.Local.OfType<K>().Any(predicate) || _set.OfType<K>().Any(predicate);
        }

        public virtual T Get(params object[] keyValues)
        {
            return _set.Find(keyValues);
        }

        public void UpdateChildCollection<TChild, Tkey>(T entity, Expression<Func<T, IEnumerable<TChild>>> property, IEnumerable<TChild> modifiedCollection, Action<TChild, TChild> updater)
            where TChild: IEntity<Tkey>
        {
            var dbEntry = _context.Entry(entity);

            var propertyName = property.GetPropertyAccess().Name;
            var dbItemsEntry = dbEntry.Collection(propertyName);
            var accessor = dbItemsEntry.Metadata.GetCollectionAccessor();

            var dbItemsMap = ((IEnumerable<TChild>)dbItemsEntry.CurrentValue).ToDictionary(e => e.Id);

            foreach (var item in modifiedCollection)
            {
                if (!dbItemsMap.TryGetValue(item.Id, out var oldItem))
                {
                    accessor.Add(entity, item, true);
                    _context.Add(item);
                }
                else
                {
                    
                    updater(oldItem, item);
                    //_context.Entry(oldItem).CurrentValues.SetValues(updatedItem);
                    dbItemsMap.Remove(item.Id);
                }
            }

            foreach (var oldItem in dbItemsMap.Values)
                accessor.Remove(entity, oldItem);
        }
    }
}
