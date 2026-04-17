using Mapster;
using Microsoft.EntityFrameworkCore;
using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.DTO.Response;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;
        public SubscriptionRepository(ApplicationDbContext context):base(context) {
            _context = context;
        }


        // يرجع الاشتراك لهذا المراجع
        public async Task<Subscription?> GetActiveSubscriptionAsync(string clientId)
        {
            return await GetOne(
         filter: s => s.ClientId == clientId  &&
                      (s.Status == SubscriptionStatus.Pending ||
                       s.Status == SubscriptionStatus.Active)
     );
        }

        //يرجع كل المشتركين مع هذا الاخصائي
        public async Task<List<Subscription>> GetClientsByNutritionistAsync(string nutritionistId)
        {
            var all = await GetAllAsync(includes: new[] { "Client" });

            return all.Where(s => s.NutritionistId == nutritionistId &&
                                  s.Status == SubscriptionStatus.Active)
                      .ToList();
        }

        //يرجغ طلبات الاشتراك مع هذا الاخصائي
        public async Task<List<NutritionistSubscriptionRequestsResponse>> GetNutriRequests (string nutritionistId)
        {
            var result = await _context.Subscriptions.Include(s => s.Client)
                .Where(s => s.NutritionistId == nutritionistId
                && s.Status == SubscriptionStatus.Pending)
                .Select(s => new NutritionistSubscriptionRequestsResponse
                {
                    SubscriptionId = s.SubscriptionId,
                    ClientName = s.Client.FullName,
                    PlanId = s.UserPlan,
                    Notes = s.Notes
                }).ToListAsync();

            return result;
            
        }

       
        public async Task<Subscription?> GetByIdAsync(int subscriptionId)
        {
            return await GetOne(
                filter: s => s.SubscriptionId == subscriptionId,
                includes: new[] { "Client", "Nutritionist" }
            );
        }

        public async Task<Subscription> UpdateAsync(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<List<SubscriptionHistory>> ClientSubscriptionHistory(string ClientId)
        {
            var subscriptions = await _context.Subscriptions.Include(s=> s.Client).Include(s=> s.Nutritionist)
                .Where(s=> s.ClientId ==  ClientId).ToListAsync();

            return subscriptions.Adapt<List<SubscriptionHistory>>();
        }

        public async Task<List<SubscriptionHistory>> NutritionistSubscriptionHistory(string nutriID)
        {
            var subscriptions = await _context.Subscriptions.Include(s => s.Client).Include(s => s.Nutritionist)
                .Where(s => s.NutritionistId == nutriID).ToListAsync();

            return subscriptions.Adapt<List<SubscriptionHistory>>();
        }

    }
}
