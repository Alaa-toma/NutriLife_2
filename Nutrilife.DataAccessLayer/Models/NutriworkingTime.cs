using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class NutriworkingTime
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public TimeOnly Time { get; set; }
        public Nutritionist nutritionist { get; set; }
    }
}
