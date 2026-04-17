using Mapster;
using Nutrilife.DataAccessLayer.DTO.Response;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.LogicLayer.Mapping
{
    public static class MappsterConfig
    {
        public static void MappsterConfigRegister()
        {
            TypeAdapterConfig<Client, ClientResponse>.NewConfig().Map(dest => dest.Client_id , source=> source.Id);


            TypeAdapterConfig<Subscription, NutritionistSubscriptionRequestsResponse>.NewConfig()
                .Map(dest => dest.PlanId, source => source.UserPlan);

            TypeAdapterConfig<Subscription, SubscriptionHistory>.NewConfig()
                .Map(dest => dest.subscriptionId , source => source.SubscriptionId)
                .Map(dest=> dest.ClientName, source=> source.Client.FullName)
                .Map(dest => dest.NutritionistName, source => source.Nutritionist.FullName);


        }
    }
}
