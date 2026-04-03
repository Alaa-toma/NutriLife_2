using Microsoft.EntityFrameworkCore;
using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public class NutritionistPlansRepository : GenericRepository<NutritionistPlans>, INutritionistPlansRepository
    {

        private readonly ApplicationDbContext _context;
        public NutritionistPlansRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<NutritionistPlans> GetByIdAsync(int planId)
        {
            return await _context.NutritionistPlans.FindAsync(planId); 
        }


        public async Task<List<NutritionistPlans>> MyPlans(string nutriId)
        {
            return await _context.NutritionistPlans.Where(p => p.nutritionistId == nutriId).ToListAsync();
        }

    }
}
