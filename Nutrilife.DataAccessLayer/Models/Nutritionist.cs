using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class Nutritionist : ApplicationUser
    {
        public string Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public string Bio { get; set; }
        public string? Location { get; set; }
        public List<string> Languages { get; set; }
        public string WorkingTime { get; set; }
        public List<string?> Certifications { get; set; }
        public List<string?> ExpertIn { get; set; }
        public List<NutritionistPlans> plans { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
