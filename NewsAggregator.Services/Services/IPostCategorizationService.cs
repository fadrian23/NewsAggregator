using NewsAggregator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Services.Services
{
    public interface IPostCategorizationService
    {
        IPost CategorizePost(IPost post);
    }
}
