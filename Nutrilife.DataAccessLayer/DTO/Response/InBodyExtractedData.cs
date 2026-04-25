using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Response
{
    public class InBodyExtractedData
    {
        public InBodyFieldValue Weight { get; set; } = new();
        public InBodyFieldValue BMI { get; set; } = new();
        public InBodyFieldValue BodyFatPercentage { get; set; } = new();
        public InBodyFieldValue MuscleMass { get; set; } = new();
        public InBodyFieldValue FatMass { get; set; } = new();
        public InBodyFieldValue VisceralFatLevel { get; set; } = new();
        public InBodyFieldValue TotalBodyWater { get; set; } = new();
        public InBodyFieldValue BasalMetabolicRate { get; set; } = new();
        public InBodyFieldValue BoneMineralContent { get; set; } = new();
        public InBodyFieldValue RightArmMuscle { get; set; } = new();
        public InBodyFieldValue LeftArmMuscle { get; set; } = new();
        public InBodyFieldValue RightLegMuscle { get; set; } = new();
        public InBodyFieldValue LeftLegMuscle { get; set; } = new();
    }

    public class InBodyFieldValue
    {
        public float? Value { get; set; }
        public float Confidence { get; set; }
        public bool NeedsReview => Confidence < 0.75f && Value != null;
    }

}
