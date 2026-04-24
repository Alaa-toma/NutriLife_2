using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{
    public class UpdateClientRequest
    {
        public string UserName { get; set; }
        public string FullName { get; set; } 
        public string? Gender { get; set; }
        public DateOnly DOF { get; set; }
        public string? PhoneNumber { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }

        public string? Disease { get; set; }
        public string? Goal { get; set; }
    }
}
