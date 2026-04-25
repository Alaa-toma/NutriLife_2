using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{
    public  class HealthTrackingRequest
    {
        public string ClientId { get; set; }
        public int? subscriptionID { get; set; }
        public role creatorRole { get; set; }
        public TrackingType type { get; set; }
        public ManualMeasurement? manualMeasurement { get; set; }
    }
}
