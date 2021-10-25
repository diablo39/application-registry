﻿using ApplicationRegistry.Domain.Persistency;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Domain.Persistency
{
    internal class RepositoryBase<T> : IRepository<T>
        where T: class
    {
        protected DbSet<T> _set;

        public RepositoryBase(DbContext context)
        {
            _set = context.Set<T>();
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
    }
}
