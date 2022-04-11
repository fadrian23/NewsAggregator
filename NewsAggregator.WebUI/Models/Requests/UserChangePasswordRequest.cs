namespace NewsAggregator.WebUI.Models.Requests
{
    public class UserChangePasswordRequest
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
