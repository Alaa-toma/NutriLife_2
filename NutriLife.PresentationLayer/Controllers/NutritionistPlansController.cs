using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.DataAccessLayer.Models;
using Nutrilife.LogicLayer.Service;

namespace NutriLife.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    // (Roles = "Nutritionist")
    public class NutritionistPlansController : ControllerBase
    {
        private readonly INutritionistService _nutritionistService;

        public NutritionistPlansController(INutritionistService nutritionistService) 
        {
            _nutritionistService = nutritionistService;
        }


        [HttpGet("MyPlans/{NutriId}")]
        public async Task<IActionResult> GetAllPlans(string NutriId)
        {
            var result = await _nutritionistService.MyPlans(NutriId);
            if(result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("deletePlan/{Id}")]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> DeletePlan(int Id)
        {
            var result = await _nutritionistService.DeletePlanAsync(Id);
            if(result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("createPlan")]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> AddPlan(NutritionistPlansRequest request)
        {
            var result = await _nutritionistService.CreatPlanAsync(request);
            if(result == null)
            {
                return BadRequest(result);
            }
           return Ok(result);
        }

        [HttpPut("editPlan/{id}")]
        [Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> EditPlan(int id, NutritionistPlansRequest request)
        {
            var result = await _nutritionistService.UpdatePlanAsync(id, request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
