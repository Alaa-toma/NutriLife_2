using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class MealLog
    {
        public Guid id {  get; set; }
        public Guid ScheduledMealId { get; set; }
        public Guid PlanOfDayId { get; set; }
        public string clientId { get; set; }
        public LogStatus status { get; set; }
        public string? CustomMealName { get; set; }
        public string? CustomMealDescription { get; set; }
        public ScheduledMeal? ScheduledMeal { get; set; }
        public PlanOfDay? planDay   { get; set; }

    }

    public enum LogStatus
    {
        Eaten,
        Substituted, //استخدام وجبة بديلة
        Skipped,
        Extra
    }
}
