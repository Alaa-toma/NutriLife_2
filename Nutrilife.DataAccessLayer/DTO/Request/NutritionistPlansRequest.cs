using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{
    public class NutritionistPlansRequest
    {
        public string Title { get; set; }
        public decimal price { get; set; }
        public List<string> Description { get; set; }
        public string nutritionistId { get; set; }
    }
}
