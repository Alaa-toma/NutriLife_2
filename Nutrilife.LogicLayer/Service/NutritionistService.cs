using Azure.Core;
using Mapster;
using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.DataAccessLayer.DTO.Response;
using Nutrilife.DataAccessLayer.Models;
using Nutrilife.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.LogicLayer.Service
{
    public class NutritionistService :INutritionistService
    {
        private readonly INutritionistPlansRepository _nutriPlanRepository;
        private readonly INutritionistRepository _nutritionistRepository;

        public NutritionistService(INutritionistPlansRepository nutriPlanRepository,
            INutritionistRepository nutritionistRepository) 
        {
            _nutriPlanRepository = nutriPlanRepository;
            _nutritionistRepository = nutritionistRepository;
        }

        public async Task<NutritionistPlansResponse> CreatPlanAsync(NutritionistPlansRequest request)
        {
            var exist = await _nutritionistRepository.GetByIdAsync(request.nutritionistId);
            if(exist == null)
            {
                throw new Exception(message: "Nutritionist Not Found");
            }

            var plan = request.Adapt<NutritionistPlans>();
            var result = await _nutriPlanRepository.CreateAsync(plan);
           return result.Adapt<NutritionistPlansResponse>();

        }

        public async Task<NutritionistPlansResponse> UpdatePlanAsync(int id, NutritionistPlansRequest request)
        {

            var exist = await _nutriPlanRepository.GetByIdAsync(id);
            if(exist == null)
            {
                throw new Exception(message: "Plan Not Found");
            }
            request.Adapt(exist);

            var result = await _nutriPlanRepository.UpdateAsync(exist);
            return result.Adapt<NutritionistPlansResponse>();
        }

       public async Task<MessageResponse> DeletePlanAsync(int id)
        {
            var exist = await _nutriPlanRepository.GetByIdAsync(id);
            if (exist == null)
            {
                throw new Exception(message: "Plan Not Found");
            }
            var deleted = await _nutriPlanRepository.deleteAsync(exist);
            if (!deleted) { return new MessageResponse() { success= false, message="Faild"}; }

            return new MessageResponse() { success = true, message = "deleted successfully" };  
        }

        public async Task<List<NutritionistPlans>> MyPlans(string NutriId)
        {
            var exist = await _nutritionistRepository.GetByIdAsync(NutriId);
            if (exist == null)
            {
                throw new Exception(message: "Nutritionist Not Found");
            }

            return await _nutriPlanRepository.GetAllAsync();
        }


    }
}
