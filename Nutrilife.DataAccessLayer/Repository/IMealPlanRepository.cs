using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public interface IMealPlanRepository : IGenericRepository<MealPlan>
    {
        Task<bool> ActivatePlanAsync(Guid planId);
        Task<IEnumerable<MealPlan>> GetPlansByClientAsync(string clientId);
        Task<IEnumerable<MealPlan>> GetPlansByNutritionistAsync(string nutritionistId);
        Task<MealPlan?> GetPlanByIdAsync(Guid planId);
    }
}
