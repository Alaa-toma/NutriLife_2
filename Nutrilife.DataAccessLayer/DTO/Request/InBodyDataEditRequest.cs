using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{
    public class InBodyDataEditRequest
    {
        public int InBodyScanId { get; set; }
        public float? weight { get; set; }
        public float? BMI { get; set; }
        public float? BodyFatPercentage { get; set; } // نسبة الدهون في الجسم
        public float? MuscleMass { get; set; } //كتلة العضلات
        public float? FatMass { get; set; } // كتلة الدهون
        public float? VisceralFatLevel { get; set; } // مستوى الدهون الحشوية
        public float? TotalBodyWater { get; set; } //إجمالي سوائل الجسم
        public float? BasalMetabolicRate { get; set; } // معدل الأيض الأساسي
        public float? BoneMineralContent { get; set; } //محتوى المعادن العظمية
        public float? RightArmMuscle { get; set; } 
        public float? LeftArmMuscle { get; set; } 
        public float? RightLegMuscle { get; set; } 
        public float? LeftLegMuscle { get; set; } 
    }
}
