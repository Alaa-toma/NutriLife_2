using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    public class NutritionistSubscriptionRequestsResponse
    {
        public int SubscriptionId {  get; set; }
        public string ClientName { get; set; }
        public int PlanId { get; set; }
        public string Notes { get; set; }

    }
}
