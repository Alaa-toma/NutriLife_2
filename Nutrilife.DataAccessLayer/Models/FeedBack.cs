using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class FeedBack
    {
        public int Id {  get; set; }
        public int? subscriptionId { get; set; }
        public string content { get; set; }
        public int rate { get; set; }
        public DateOnly date {  get; set; }

        public Subscription? Subscription { get; set; }
    }
}
