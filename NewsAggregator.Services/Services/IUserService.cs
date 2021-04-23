using NewsAggregator.Services.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Services
{
    public interface IUserService
    {
        Task<UserServiceResult> ChangeNameAsync(string newUsername, string userId);
        Task<UserServiceResult> ChangePasswordAsync(string oldPassword, string newPassword, string userId);
    }
}
