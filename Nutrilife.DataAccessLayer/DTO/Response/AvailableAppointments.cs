using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    public class AvailableAppointments
    {
        public DateOnly date { get; set; }
        public TimeOnly Time { get; set; }
        public string? Notes { get; set; }

    }
}
