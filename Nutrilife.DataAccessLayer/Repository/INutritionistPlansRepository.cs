using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public interface INutritionistPlansRepository :IGenericRepository<NutritionistPlans>
    {
    Task<NutritionistPlans> GetByIdAsync(int PlanId);
    }
}
