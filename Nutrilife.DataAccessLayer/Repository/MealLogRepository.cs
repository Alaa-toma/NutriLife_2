using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public class MealLogRepository: GenericRepository<MealLog>, IMealLogRepository
    {
        private readonly ApplicationDbContext _context;
        public MealLogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
