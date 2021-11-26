namespace NewsAggregator.Services.Services
{
    public interface ISiteFactory
    {
        ISiteService For(string userSelection);

        IRssSitesService ForRssSite(string userSelection);
    }
}