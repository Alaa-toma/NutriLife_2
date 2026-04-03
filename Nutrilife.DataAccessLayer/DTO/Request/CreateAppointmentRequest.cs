using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{
    public class CreateAppointmentRequest
    {
        public DateOnly date { get; set; }
        public TimeOnly Time { get; set; }
        public string? Notes { get; set; }
    }
}
