using ApplicationRegistry.Infrastructure.Abstractions.Scheduler;
using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;
using System.Linq.Expressions;

namespace ApplicationRegistry.Infrastructure.Implementations.Scheduler
{
    public class Scheduler : IScheduler
    {
        public void Enqueue<T>(Expression<Action<T>> expression)
        {
            BackgroundJob.Enqueue(expression);
        }
    }
}
