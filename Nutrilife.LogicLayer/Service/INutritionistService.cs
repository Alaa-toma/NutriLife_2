using Mapster;
using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.DataAccessLayer.DTO.Response;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.LogicLayer.Service
{
    public interface INutritionistService
    {
        Task<NutritionistPlansResponse> CreatPlanAsync(NutritionistPlansRequest request);
        Task<NutritionistPlansResponse> UpdatePlanAsync(int id, NutritionistPlansRequest request);
        Task<MessageResponse> DeletePlanAsync(int id);
         Task<List<NutritionistPlansResponse>> MyPlans(string NutriId);
        Task<NutritionistPlansResponse> getOne(int planID);

    }
}
