using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.DataAccessLayer.DTO.Response;
using Nutrilife.LogicLayer.Service;

namespace NutriLife.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackService _feedBackService;

        public FeedBackController(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }

        [HttpGet("AllNutriFeddbacks/{nutriId}")]
        public async Task<IActionResult> AllNutritionistFeedBacks(string nutriId)
        {
            var result = await _feedBackService.AllNutritionistFeedBacks(nutriId);
            if(result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPost("AddFeddback")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> AddFeedBack(FeedBackRequest request)
        {
            var result = await _feedBackService.AddFeedBack(request);
            if (result == null) { return BadRequest(result); }
            return Ok(result);
        }

        [HttpPost("DeleteFeddback/{FeedBackId}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> DeleteFeddback(int FeedBackId)
        {
            var result = await _feedBackService.DeleteFeedBack(FeedBackId);
            if (result == false) { return BadRequest(result); }
            return Ok(result);
        }


    }
}
