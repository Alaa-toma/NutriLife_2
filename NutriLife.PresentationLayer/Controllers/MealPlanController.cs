using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.LogicLayer.Service;
using System.Security.Claims;

namespace NutriLife.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly IMealPlanService _mealPlanService;

        public MealPlanController(IMealPlanService mealPlanService) 
        {
            _mealPlanService = mealPlanService;
        }

        [HttpPost]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> CreatePlan( MealPlanRequest request)
        {
           
            var result = await _mealPlanService.CreatePlanAsync(request);
           
            if(result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }


        [HttpPost("days")]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> AddDay( [FromBody] AddPlanDayRequest request)
        {
            var result = await _mealPlanService.AddDayAsync( request);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }


        [HttpPost("AddMeal")]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> AddMeal([FromBody] AddScheduledMealRequest request)
        {
            var result = await _mealPlanService.AddMealAsync(request);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        // -------------------------------------------------------


        [HttpPut("Updatemeal")]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> UpdateMeal( [FromBody] UpdateScheduledMealRequest request)
        {
            var result = await _mealPlanService.UpdateMealAsync(request);
            if (result is null) return NotFound("Meal not found.");
            return Ok(result);
        }



        [HttpDelete("Deletemeal/{scheduledMealId}")]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> DeleteMeal(Guid scheduledMealId)
        {
            var success = await _mealPlanService.DeleteMealAsync(scheduledMealId);
            if (!success) return NotFound("Meal not found.");
            return NoContent();
        }

        //--------------------------------------

        [HttpDelete("Deleteday/{planOfDayId}")]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> DeleteDay(Guid planOfDayId)
        {
            var success = await _mealPlanService.DeleteDayAsync(planOfDayId);
            if (!success) return NotFound("Day not found...");
            return Ok();
        }


        [HttpPut("activate/{planId}")]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> ActivatePlan(Guid planId)
        {
            var success = await _mealPlanService.ActivatePlanAsync(planId);
            if (!success) return NotFound("Plan not found.");
            return NoContent();
        }


        [HttpGet("nutritionistPlans")]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> GetMyPlansAsNutritionist()
        {
            var result = await _mealPlanService.GetPlansByNutritionistAsync();
            return Ok(result);
        }

        // -----------------------------------

        [HttpGet("getplan/{planId}")]
        [Authorize]
        public async Task<IActionResult> GetPlanById(Guid planId)
        {
            var result = await _mealPlanService.GetPlanByIdAsync(planId);
            if (result is null) return NotFound("Plan not found.");
            return Ok(result);
        }



        [HttpGet("clientPlansS/{clientID}")]
        [Authorize]
        public async Task<IActionResult> GetMyPlansAsClient(string clientID)
        {
            
            var result = await _mealPlanService.GetPlansByClientAsync(clientID);
            return Ok(result);
        }


        [HttpGet("MyclientPlansS/{clientID}")]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> GetClientPlansByNutriAsync(string clientID)
        {
            
            var result = await _mealPlanService.GetClientPlansByNutriAsync(clientID);
            return Ok(result);
        }



        [HttpPost("mealslog")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> LogMeal( [FromBody] LogMealRequest request)
        {
            var result = await _mealPlanService.LogMealAsync(request);
            return Ok(result);
        }

       




    }
}
