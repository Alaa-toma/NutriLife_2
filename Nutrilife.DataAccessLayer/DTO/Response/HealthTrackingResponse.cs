using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    public class HealthTrackingResponse
    {
        public string ClientId { get; set; }
        public string NutritionistId { get; set;}
        public int subscriptionID { get; set; }
        public TrackingType type { get; set; }
        public DateTime addedin { get; set; }
        public InBodyScanResponse? inBody {  get; set; }
        public ManualMeasurementResponse? manualMeasurement { get; set; }
    }
}
