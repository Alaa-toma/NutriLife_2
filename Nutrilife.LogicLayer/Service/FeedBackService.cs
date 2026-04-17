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
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.LogicLayer.Service
{
    public class FeedBackService : IFeedBackService
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _UserManager;

        public FeedBackService(IFeedBackRepository feedBackRepository,
            ISubscriptionRepository subscriptionRepository,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> UserManager) 
        {
            _feedBackRepository = feedBackRepository;
            _subscriptionRepository = subscriptionRepository;
            _httpContextAccessor = httpContextAccessor;
            _UserManager    = UserManager;
        }


        private string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<FeedBackResponse> AddFeedBack(FeedBackRequest request)
        {
            var clientId = GetCurrentUserId();

            var subscription = await _subscriptionRepository.GetByIdAsync(request.subscriptionId)
             ?? throw new Exception("Subscription not found.");

            if (subscription.ClientId != clientId)
            { throw new UnauthorizedAccessException("This subscription does not belong to you."); }


            if (request.rate < 1 || request.rate > 5)
            { throw new ArgumentException("Rating must be between 1 and 5."); }

            if (subscription.Status == SubscriptionStatus.Pending)
            { throw new Exception("Your subscription request is still pending. You can leave feedback once it is activated."); }

            // يرجع ترو اذا في تعليق سابق
            var existingFeedback = await _feedBackRepository.GetBySubscriptionIdAsync(request.subscriptionId);
            if (existingFeedback != null)
            {
                throw new Exception("You have already submitted feedback for this subscription.");
            }

            if (string.IsNullOrWhiteSpace(request.content))
                throw new ArgumentException("Comment cannot be empty.");

            if (request.content.Length > 500)
                throw new ArgumentException("Comment cannot exceed 500 characters.");

            var nutritionist = await _UserManager.FindByIdAsync(subscription.NutritionistId)
                ?? throw new Exception("Nutritionist not found.");


            var feedback = request.Adapt<FeedBack>();
            feedback.date = DateOnly.FromDateTime(DateTime.UtcNow);

            var created = await _feedBackRepository.CreateAsync(feedback);

            return(created.Adapt<FeedBackResponse>());
        }

        public async Task<List<FeedBackResponse>> AllNutritionistFeedBacks(string nutriId)
        {
            var nutri = await _UserManager.FindByIdAsync(nutriId);
            if(nutri == null)
            {
                throw new Exception("Nutritionist Nit Found!");
            }

            var isNutritionist = await _UserManager.IsInRoleAsync(nutri, "Nutritionist");
            if (!isNutritionist)
            {
                throw new Exception("This user is not a nutritionist.");
            }

            var comments = await _feedBackRepository.GetByNutritionistIdAsync(nutriId);

            return comments.Adapt<List<FeedBackResponse>>();
        }

        public async Task<bool> DeleteFeedBack(int id)
        {
            var feedback = await _feedBackRepository.GetOne(f => f.Id == id)
                 ?? throw new Exception("Feedback not found.");

          var deleted =  await _feedBackRepository.deleteAsync(feedback);
           
            return deleted;
        }
    }
}
