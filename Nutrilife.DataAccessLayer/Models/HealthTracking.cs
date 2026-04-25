using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class HealthTracking
    {
        public int Id { get; set; }
        public string ClientId  { get; set; }
        public Client client { get; set; }
        public int SubscriptioId    { get; set; }

        public role creatorRole { get; set; }
        public Subscription subscription { get; set; }

        public TrackingType type {  get; set; }

        public ManualMeasurement? manualMeasurement { get; set; }
        public InBodyScan? InBodyScan { get; set; }
    }

    public enum TrackingType
    {
        Manual, 
        InBody, 
        Both
    }
    public enum role
    {
        Client, Nutritionist
    }

}
