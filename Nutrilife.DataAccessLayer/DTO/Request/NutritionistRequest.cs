using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.DTO.Request
{
    public class NutritionistRequest : RegisterRequest
    {
        public string Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public string Bio { get; set; }
        public string? Location { get; set; }
        public List<string> Languages { get; set; }
        public string WorkingTime { get; set; }
        public List<string?> Certifications { get; set; }
        public List<string?> ExpertIn { get; set; }

    }
}
