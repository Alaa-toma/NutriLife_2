using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class ManualMeasurement
    {
        public int id {  get; set; }
        public int HealthTrackingId { get; set; }
        public HealthTracking healthTracking { get; set; } = null!;

        public float? Weight { get; set; }
        public float? Height { get; set; }
        public float? WaistCircumference { get; set; } //محيط الخصر
        public float? HipCircumference { get; set; } //محيط الورك
        public float? ChestCircumference { get; set; } //محيط الصدر
        public float? ArmCircumference { get; set; } //محيط الذراع
        public float? ThighCircumference { get; set; } //محيط الفخذ

        public DateTime MeasuredAt { get; set; } = DateTime.UtcNow;

    }
}
