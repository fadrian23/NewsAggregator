using System.ComponentModel.DataAnnotations;

namespace NewsAggregator.WebUI.Models.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
