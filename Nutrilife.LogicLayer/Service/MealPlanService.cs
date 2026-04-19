using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.DataAccessLayer.DTO.Response;
using Nutrilife.DataAccessLayer.Models;
using Nutrilife.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ScheduledMeal = Nutrilife.DataAccessLayer.Models.ScheduledMeal;

namespace Nutrilife.LogicLayer.Service
{
    public class MealPlanService : IMealPlanService
    {
        private readonly IMealPlanRepository _mealPlanRepository;
        private readonly IPlanOfDayRepository _PlanOfDayRepository;
        private readonly ISCheduled_Meals _scheduledMeal;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IMealLogRepository _mealLogRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public MealPlanService(IMealPlanRepository mealPlanRepository, 
           IPlanOfDayRepository PlanOfDayRepository,
           ISCheduled_Meals scheduledMeal, IHttpContextAccessor httpContextAccessor,
           UserManager<ApplicationUser> UserManager , 
           IMealLogRepository mealLogRepository, ISubscriptionRepository subscriptionRepository) 
        {
            _mealPlanRepository = mealPlanRepository;
            _PlanOfDayRepository = PlanOfDayRepository;
            _scheduledMeal = scheduledMeal;
            _httpContextAccessor = httpContextAccessor;
            _UserManager = UserManager;
            _mealLogRepository = mealLogRepository;
            _subscriptionRepository = subscriptionRepository;
        }


        private string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<MealPlanResponse> CreatePlanAsync(MealPlanRequest request)
        {
            var nutriID = GetCurrentUserId();
            var nutri = await _UserManager.FindByIdAsync(nutriID);

            var subscriptoion = await _subscriptionRepository.GetOne(s=> s.ClientId == request.ClientId
            && s.NutritionistId == nutriID && s.Status == SubscriptionStatus.Active);
            if (subscriptoion == null)
            {
                throw new Exception("This Client Does't hava Active Subscription with you..!");
            }

          var mealPlan =   request.Adapt<MealPlan>();
            mealPlan.NutritionistId = nutriID;
            var created = await _mealPlanRepository.CreateAsync(mealPlan);

            return created.Adapt<MealPlanResponse>();
        }

        public async Task<DayResponse> AddDayAsync( AddPlanDayRequest request)
        {
            var day = request.Adapt<PlanOfDay>();

            var created = await _PlanOfDayRepository.CreateAsync(day);

            return created.Adapt<DayResponse>();
        }

        public async Task<ScheduledMealResponse> AddMealAsync(AddScheduledMealRequest request)
        {

            var sch_meal = request.Adapt<ScheduledMeal>();
            var created = await _scheduledMeal.CreateAsync(sch_meal);
            return created.Adapt<ScheduledMealResponse>();
        }

        public async Task<ScheduledMealResponse?> UpdateMealAsync(Guid scheduledMealId, UpdateScheduledMealRequest request)
        {
            var meal = await _scheduledMeal.GetOne(a=> a.Id == scheduledMealId);
            var updated = await _scheduledMeal.UpdateAsync(meal);
            if(updated == null) { throw new Exception("Update Failed!"); }
            return updated.Adapt<ScheduledMealResponse?>();
        }

        public async Task<bool> DeleteMealAsync(Guid scheduledMealId)
        {
            var meal = await _scheduledMeal.GetOne(a => a.Id == scheduledMealId);
            if(meal == null)
            {
                throw new Exception("Meal Not Found!");
            }
             var deleted = await _scheduledMeal.deleteAsync(meal);

            if(deleted) { return false; }
            return true;
        }

        public async Task<bool> DeleteDayAsync(Guid planOfDayId)
        {

            var day = await _PlanOfDayRepository.GetOne(a=> a.Id==planOfDayId);
            if (day == null)
            {
                throw new Exception("Day Not Found!");
            }
            var deleted = await _PlanOfDayRepository.deleteAsync(day);
            if (deleted) { return false; }
            return true;
        }


        public async Task<bool> ActivatePlanAsync(Guid planId)
        {
            return await _mealPlanRepository.ActivatePlanAsync(planId);
        }

        public async Task<MealPlanResponse?> GetPlanByIdAsync(Guid planId)
        {

            var plan = await _mealPlanRepository.GetPlanByIdAsync(planId);
            
            if(plan == null)
            {
                throw new Exception("Plan Not Found!");
            }
            return plan.Adapt<MealPlanResponse>();
        }



        public async Task<IEnumerable<MealPlanSummaryResponse>> GetPlansByClientAsync(string clientId)
        {

            var plans = await _mealPlanRepository.GetPlansByClientAsync(clientId);
            if (plans == null)
            {
                throw new Exception("No Plans Exist for this client..!");
            }
            return plans.Adapt<IEnumerable < MealPlanSummaryResponse >> ();
        }

        public async Task<IEnumerable<MealPlanSummaryResponse>> GetClientPlansByNutriAsync(string clientId)
        {
            var nutriId = GetCurrentUserId();

            var subscription = await _subscriptionRepository.GetOne(s => s.ClientId == clientId && s.NutritionistId == nutriId);

            
            if (clientId != subscription.ClientId && nutriId != subscription.NutritionistId)
            {
                throw new Exception("You cannot access this plan..!");
            }

            var plans = await _mealPlanRepository.GetPlansByClientAsync(clientId);
            if (plans == null)
            {
                throw new Exception("No Plans Exist for this client..!");
            }
            return plans.Adapt<IEnumerable<MealPlanSummaryResponse>>();
        }


        public async Task<IEnumerable<MealPlanSummaryResponse>> GetPlansByNutritionistAsync()
        {
            var nutriId =  GetCurrentUserId();
            var plans = await _mealPlanRepository.GetPlansByNutritionistAsync (nutriId);
            if (plans == null)
            {
                throw new Exception("No Plans Exist for this client..!");
            }
            return plans.Adapt<IEnumerable<MealPlanSummaryResponse>>();
        }


        public async Task<MealLogResponse> LogExtraMealAsync(LogExtraMealRequest request) // وجبة اضافية, 
        {
            var Extra = request.Adapt<MealLog>();
            var created = await _mealLogRepository.CreateAsync(Extra);
            return created.Adapt<MealLogResponse>();
        }



        public async Task<MealLogResponse> LogMealAsync(LogMealRequest request)
        {
            var existiong = await _mealLogRepository.GetOne(a => a.ScheduledMealId == request.ScheduledMealId); // تم اضافة ؤد على هذا اليوم؟
            if (existiong != null)
            {
                existiong.Adapt(request); // edit status, meal....>
                var updated = await _mealLogRepository.UpdateAsync(existiong);
                return updated.Adapt<MealLogResponse>();
            }
            var mealLOg = request.Adapt<MealLog>();
            var created = await _mealLogRepository.CreateAsync(mealLOg);
            return created.Adapt<MealLogResponse>();
        }

    }
}
