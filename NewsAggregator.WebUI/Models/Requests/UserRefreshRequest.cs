namespace NewsAggregator.WebUI.Models.Requests
{
    public class UserRefreshRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}