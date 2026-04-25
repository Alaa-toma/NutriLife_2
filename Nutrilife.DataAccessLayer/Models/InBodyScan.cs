using Nutrilife.DataAccessLayer.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class InBodyScan
    {
        public int Id { get; set; }
        public int HealthTrackingId { get; set; }
        public HealthTracking healthTracking { get; set; } = null!;

        public string filePath { get; set; } = string.Empty; // inbody file 
        public string? RowAIOutput { get; set; } // store json from ai


        //needed values
        public float weight { get; set; }
        public float BMI {  get; set; }
        public float BodyFatPercentage { get; set; } // نسبة الدهون في الجسم
        public float MuscleMass { get; set; } //كتلة العضلات
        public float FatMass { get; set; } // كتلة الدهون
        public float VisceralFatLevel { get; set; } // مستوى الدهون الحشوية
        public float TotalBodyWater { get; set; } //إجمالي سوائل الجسم
        public float BasalMetabolicRate { get; set; } // معدل الأيض الأساسي
        public float BoneMineralContent { get; set; } //محتوى المعادن العظمية
        public float RightArmMuscle { get; set; } 
        public float LeftArmMuscle { get; set; } 
        public float RightLegMuscle { get; set; }
        public float LeftLegMuscle { get; set; } 
        public DateTime ScannedAt { get; set; } = DateTime.UtcNow;


    }
}
