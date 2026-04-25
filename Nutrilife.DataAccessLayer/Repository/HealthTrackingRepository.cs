using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public class HealthTrackingRepository : GenericRepository<HealthTracking>, IHealthTrackingRepository
    {

        private readonly ApplicationDbContext _context;
        public HealthTrackingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }




    }
}
