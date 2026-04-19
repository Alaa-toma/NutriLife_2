using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.DataAccessLayer.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.LogicLayer.Service
{
    public interface IMealPlanService
    {

        // ── Nutritionist ─────────────────────────────────────────────────────────
        Task<MealPlanResponse> CreatePlanAsync( MealPlanRequest request);
        Task<DayResponse> AddDayAsync(AddPlanDayRequest request);
        Task<ScheduledMealResponse> AddMealAsync(AddScheduledMealRequest request);
        Task<ScheduledMealResponse?> UpdateMealAsync(Guid scheduledMealId, UpdateScheduledMealRequest request);
        Task<bool> DeleteMealAsync(Guid scheduledMealId);
        Task<bool> DeleteDayAsync(Guid planOfDayId);
        Task<bool> ActivatePlanAsync(Guid planId);

        // ── Shared ────────────────────────────────────────────────────────────────
        Task<MealPlanResponse?> GetPlanByIdAsync(Guid planId);
        Task<IEnumerable<MealPlanSummaryResponse>> GetPlansByClientAsync(string clientId);
        Task<IEnumerable<MealPlanSummaryResponse>> GetPlansByNutritionistAsync();
        Task<IEnumerable<MealPlanSummaryResponse>> GetClientPlansByNutriAsync(string clientId);

        // ── Client ────────────────────────────────────────────────────────────────
        Task<MealLogResponse> LogMealAsync(LogMealRequest request);
        Task<MealLogResponse> LogExtraMealAsync(LogExtraMealRequest request);


    }
}
