using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    public class SubscriptionHistory
    {
        public int subscriptionId {  get; set; }
        public string NutritionistName { get; set; }
        public string ClientName {  get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
