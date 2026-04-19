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
    public class MealPlanRepository : GenericRepository<MealPlan>, IMealPlanRepository
    {
        private readonly ApplicationDbContext _context;
        public MealPlanRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<bool> ActivatePlanAsync(Guid planId)
        {
            var plan = await _context.mealPlans.FindAsync(planId);
            if (plan is null) return false;

            plan.status = PlanStatus.Active;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MealPlan>> GetPlansByClientAsync(string clientId)
        {
            return await _context.mealPlans
                .Where(p => p.ClientId == clientId && p.status != PlanStatus.Draft)
                .Include(p => p.Days)
                .OrderByDescending(p => p.StartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<MealPlan>> GetPlansByNutritionistAsync(string nutritionistId)
        {
            return await _context.mealPlans
                .Where(p => p.NutritionistId == nutritionistId)
                .Include(p => p.Days)
                    .ThenInclude(d => d.meals)
                .OrderByDescending(p => p.StartDate)
                .ToListAsync();
        }

        public async Task<MealPlan?> GetPlanByIdAsync(Guid planId)
        {
            return await _context.mealPlans
                .Include(p => p.Days.OrderBy(d => d.DayNumber))
                    .ThenInclude(d => d.meals.OrderBy(m => m.OrderIndex))
                        .ThenInclude(m => m.log)
                .FirstOrDefaultAsync(p => p.Id == planId);
        }
    }
}
