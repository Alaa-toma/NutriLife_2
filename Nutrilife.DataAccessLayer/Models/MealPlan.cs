using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class MealPlan
    {
        // top level of plan (all week..month etc.. )
        public Guid Id { get; set; }
        public string  NutritionistId { get; set; }
        public string ClientId { get; set; }

        public string title { get; set; } // title of plan
        public DateOnly StartDate { get; set; } 
        public PlanStatus status { get; set; } //
        public ICollection<PlanOfDay> Days { get; set; } // days.. meals in day...>..

    }


    public enum PlanStatus
    {
        Draft,
        Active,
        Completed
    }
}
