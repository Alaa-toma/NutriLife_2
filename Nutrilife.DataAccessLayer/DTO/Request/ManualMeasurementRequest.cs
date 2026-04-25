using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{
    public class ManualMeasurementRequest
    {
        public int HealthTrackingId { get; set; }
        public float? Weight { get; set; }
        public float? Height { get; set; }
        public float? WaistCircumference { get; set; } //محيط الخصر
        public float? HipCircumference { get; set; } //محيط الورك
        public float? ChestCircumference { get; set; } //محيط الصدر
        public float? ArmCircumference { get; set; } //محيط الذراع
        public float? ThighCircumference { get; set; } //محيط الفخذ

        public DateTime MeasuredAt { get; set; } = DateTime.UtcNow;
        public role creatorRole { get; set; }

    }
}
