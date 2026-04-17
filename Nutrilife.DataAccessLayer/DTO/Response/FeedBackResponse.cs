using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    public class FeedBackResponse
    {
        public int Id { get; set; }
        public string content { get; set; }
        public int rate { get; set; }
        public DateOnly date { get; set; }
    }
}
