using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    public class SubscriptionResponse
    {
        public int SubscriptionId { get; set; }
        public string ClientId { get; set; }
        public string NutritionistId { get; set; }
        public int UserPlan { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
        public string Notes { get; set; }
    }
}
