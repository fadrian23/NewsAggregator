using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace NewsAggregator.WebApi.Filters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
