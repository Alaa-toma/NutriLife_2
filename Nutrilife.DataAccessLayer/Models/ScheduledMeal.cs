using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class ScheduledMeal
    {
        public Guid Id { get; set; }
        public Guid? PlanOfDayId { get; set; }
        public MealType MealType { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }
        public int OrderIndex { get; set; }

        public PlanOfDay? planday { get; set; }
        public MealLog? log { get; set; }

    }

    public enum MealType
    {
        Breakfast,
        Lunch,
        Dinner,
        Snack
    }
}
