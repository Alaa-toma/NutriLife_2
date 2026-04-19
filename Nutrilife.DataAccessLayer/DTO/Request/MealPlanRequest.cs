using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{
    public record MealPlanRequest // الخطة وايامها
    {
        public string ClientId { get; set; }

        public string title { get; set; } // title of plan
        public DateOnly StartDate { get; set; }
    }


    public record CreateDayRequest // اليوم ووجباته
    {
        public Guid mealPlanId { get; set; }
        public int DayNumber { get; set; }
        public string? notes { get; set; }

    }



    public record CreateScheduledMealRequest //الوجبة
    {
        public Guid? PlanOfDayId { get; set; }
        public MealType MealType { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }
        public int OrderIndex { get; set; }
    }

    public record AddPlanDayRequest //اضافة يوم لخطة موجودة
    {
        public Guid mealPlanId { get; set; }
        public int DayNumber { get; set; }
        public string? notes { get; set; }
    }

    public record AddScheduledMealRequest // اضافة وجبة ليوم موجود سابقا
    {
        public Guid mealPlanId { get; set; }
        public Guid PlanOfDayId { get; set; }
        public MealType MealType { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }
        public int OrderIndex { get; set; }
    }

    public record UpdateScheduledMealRequest //التعديل على وجبة موجودة
    {
        public Guid ScheduledMealId { get; set; }
        public MealType? MealType { get; set; }
        public string? MealName { get; set; }
        public string? MealDescription { get; set; }
        public int? OrderIndex { get; set; }
    }


    // by client ////////////////////
    public record LogMealRequest // انجازه للوجبة
    {
        
        public Guid ScheduledMealId { get; set; }
        public string clientId { get; set; }
        public LogStatus status { get; set; }
        public string? CustomMealName { get; set; } // لو اكل وجبة بديلة
        public string? CustomMealDescription { get; set; }

    }

    public record LogExtraMealRequest // اضافة وجبة ليست ضمن الخطة
    {
        public Guid ScheduledMealId { get; set; }
        public Guid clientId { get; set; }
        public Guid PlanOfDayId { get; set; }
        public string? CustomMealName { get; set; }
        public string? CustomMealDescription { get; set; }
    }

}
