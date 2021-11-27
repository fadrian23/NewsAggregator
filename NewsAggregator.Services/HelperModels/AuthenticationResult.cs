using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregator.Services.HelperModels
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public AuthenticationResultType Result { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }

    public enum AuthenticationResultType {
        WrongCombination = 0,
        UserNotFound = 1,
        LoginSuccess = 2,

        UserAlreadyExists = 3,
        RegisterSuccess = 4,
    }
}
