using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationRegistry.Infrastructure.Abstractions.Scheduler
{
    public interface IScheduler
    {
        void Enqueue<T>(Expression<Action<T>> expression);
    }
}
