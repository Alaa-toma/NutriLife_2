using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public class InBodyScanRepository : GenericRepository<InBodyScan>, IInBodyScanRepository
    {

        private readonly ApplicationDbContext _context;
        public InBodyScanRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



    }
}
