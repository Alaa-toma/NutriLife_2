using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public interface IFeedBackRepository : IGenericRepository<FeedBack>
    {
        Task<FeedBack> GetBySubscriptionIdAsync(int subscriptionId);
        Task<IEnumerable<FeedBack>> GetByNutritionistIdAsync(string nutritionistId);
    }
}
