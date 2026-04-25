using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    public class ManualMeasurementResponse
    {
        public int ManualMeasurementId { get; set; }
        public float? Weight { get; set; }
        public float? Height { get; set; }
        public float? WaistCircumference { get; set; } //محيط الخصر
        public float? HipCircumference { get; set; } //محيط الورك
        public float? ChestCircumference { get; set; } //محيط الصدر
        public float? ArmCircumference { get; set; } //محيط الذراع
        public float? ThighCircumference { get; set; } //محيط الفخذ

    }
}
