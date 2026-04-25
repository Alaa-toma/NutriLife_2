using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    public class ProgressResponse
    {
        public string ClientId { get; set; } = string.Empty;
        public int TotalSessions { get; set; }
        public List<HealthTrackingResponse> Sessions { get; set; } = new();
    }
}
