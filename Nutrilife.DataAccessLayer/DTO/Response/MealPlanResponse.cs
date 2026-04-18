using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    internal record MealPlanResponse // full plan
    {
        public Guid MealPlanId { get; set; }
        public Guid Id { get; set; }
        public string NutritionistId { get; set; }
        public string ClientId { get; set; }

        public string title { get; set; } // title of plan
        public DateOnly StartDate { get; set; }
        public PlanStatus status { get; set; } //
        public ICollection<PlanOfDay> Days { get; set; }
    }

    public record MealLogResponse
    {
        public Guid ScheduledMealId { get; set; }
        public Guid PlanOfDayId { get; set; }
        public LogStatus status { get; set; }
        public string? CustomMealName { get; set; }
        public string? CustomMealDescription { get; set; }
    }

    public record MealPlanSummaryResponse
    {
        public Guid MealPlanId { get ; set; }
        public string title { get; set; }
        public DateOnly StartDate { get; set; }
        public PlanStatus status { get; set; }
        public int totalDays { get; set; }  
        public int totalMeals { get; set; }
    }
}
