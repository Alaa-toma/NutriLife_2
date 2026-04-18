using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class PlanOfDay
    {
        // each day data and list of its  meals
        public Guid Id { get; set; }
        public Guid mealPlanId { get; set; }
        public int DayNumber { get; set; }
        public string? notes { get; set; }


        public MealPlan mealPlan { get; set; }
        public ICollection<ScheduledMeal> meals { get; set; }
    }
}
