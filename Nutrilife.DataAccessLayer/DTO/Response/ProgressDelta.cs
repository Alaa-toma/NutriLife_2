using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    public class ProgressDelta
    {
        public string Field { get; set; } = string.Empty;      // e.g. "Weight"
        public float PreviousValue { get; set; }
        public float CurrentValue { get; set; }
        public float Change { get; set; }                      // current - previous
        public string Direction { get; set; } = string.Empty; // "Up", "Down", "Same"
        public string Unit { get; set; } = string.Empty;       // "kg", "%", "kcal"
    }
}
