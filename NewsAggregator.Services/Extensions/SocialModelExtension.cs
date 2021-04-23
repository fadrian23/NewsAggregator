using NewsAggregator.Data.Models;

namespace NewsAggregator.Services.Extensions
{
    public static class SocialModelExtension
    {
        public static string GetServiceName(this ISocialModel model)
        {
            return model.GetType().Name.Replace("Post", "");
        }
    }
}
