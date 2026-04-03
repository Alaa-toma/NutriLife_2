using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{//
    public class AppointmentRequest
    {
       public int Id { get; set; } // appointment id
        public int SubscriptioId { get; set; } 
        public AppointmentType type { get; set; }

    }
}
