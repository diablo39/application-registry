using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationRegistry.Infrastructure.HangfireExtensions
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return true;
        }
    }
}
