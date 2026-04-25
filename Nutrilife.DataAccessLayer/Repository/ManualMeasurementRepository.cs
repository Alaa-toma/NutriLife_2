using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public class ManualMeasurementRepository : GenericRepository<ManualMeasurement>, IManualMeasurementRepository
    {
        private readonly ApplicationDbContext _context;
        public ManualMeasurementRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
