using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public class SCheduled_Meals : GenericRepository<ScheduledMeal>, ISCheduled_Meals
    {
        private readonly ApplicationDbContext _context;
        public SCheduled_Meals(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
