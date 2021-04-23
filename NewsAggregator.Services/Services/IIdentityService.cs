using NewsAggregator.Services.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string name, string password);
        Task<AuthenticationResult> LoginAsync(string name, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
