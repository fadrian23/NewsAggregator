using NewsAggregator.Services.HelperModels;
using NewsAggregator.WebUI.Models.Responses;

namespace NewsAggregator.WebUI.Helpers
{
    public static class AuthResponseHelper
    {
        public static FailedAuthResponse getFailedAuthResponse(AuthenticationResult result)
        {
            return new FailedAuthResponse { Result = result.Result, Errors = result.Errors, };
        }

        public static SuccessfulAuthResponse getSuccessfulAuthResponse(AuthenticationResult result)
        {
            return new SuccessfulAuthResponse
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken,
            };
        }
    }
}
