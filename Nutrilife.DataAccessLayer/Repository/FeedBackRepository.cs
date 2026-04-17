using Microsoft.EntityFrameworkCore;
using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public class FeedBackRepository :  GenericRepository<FeedBack>, IFeedBackRepository
    {

        private readonly ApplicationDbContext _context;
        public FeedBackRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // لو اليوزر علق سابقا ضمن هذا الاشتراك
        public async Task<FeedBack> GetBySubscriptionIdAsync(int subscriptionId)
        {
            return await _context.FeedBacks.FirstOrDefaultAsync(f => f.subscriptionId == subscriptionId);
            
        }

        //  قائمة بالتعليقات على صفحة هذا الاخصاءي
        public async Task<IEnumerable<FeedBack>> GetByNutritionistIdAsync(string nutritionistId)
        {
            return await _context.FeedBacks
                .Where(f => f.Subscription.NutritionistId == nutritionistId)
                .OrderByDescending(f => f.date)
                .ToListAsync();
        }
    }
}
