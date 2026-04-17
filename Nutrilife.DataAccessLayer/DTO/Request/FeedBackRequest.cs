using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{
    public class FeedBackRequest
    {
        public int subscriptionId { get; set; }
        public string content { get; set; }
        public int rate { get; set; }
    }
}
