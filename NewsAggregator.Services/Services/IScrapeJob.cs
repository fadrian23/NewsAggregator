using System.Threading.Tasks;

namespace NewsAggregator.Services.Services
{
    public interface IScrapeJob
    {
        Task ScrapeRSSFeeds();
    }
}
