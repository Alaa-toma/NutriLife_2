using Nutrilife.DataAccessLayer.DTO.Response;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public interface ISubscriptionRepository : IGenericRepository<Subscription>
    {
        Task<Subscription?> GetActiveSubscriptionAsync(string clientId);

        Task<Subscription?> GetByIdAsync(int subscriptionId); // return subscription
        Task<Subscription> UpdateAsync(Subscription subscription);
        Task<List<Subscription>> GetClientsByNutritionistAsync(string nutritionistId);
        Task<List<NutritionistSubscriptionRequestsResponse>> GetNutriRequests(string nutritionistId);
        Task<List<SubscriptionHistory>> ClientSubscriptionHistory(string ClientId);
        Task<List<SubscriptionHistory>> NutritionistSubscriptionHistory(string nutriID);
    }
}
