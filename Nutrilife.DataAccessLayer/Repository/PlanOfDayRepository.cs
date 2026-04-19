using Microsoft.EntityFrameworkCore;
using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public class PlanOfDayRepository : GenericRepository<PlanOfDay>, IPlanOfDayRepository
    {

        private readonly ApplicationDbContext _context;

        public PlanOfDayRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        


    }
}
