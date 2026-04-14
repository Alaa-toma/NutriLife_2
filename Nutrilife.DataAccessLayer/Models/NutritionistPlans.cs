using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class NutritionistPlans
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal price { get; set; }
        public int NumOfDays { get; set; }
        public List<string> Description { get; set; }
        public string nutritionistId { get; set; }
    }
}
