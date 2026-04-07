using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Models
{
    public class Appointment 
    {
        public int Id { get; set; }
        public int? SubscriptioId { get; set; }
        public AppointmentType type { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly Time { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? Notes { get; set; }
        public string? MeetingLink { get; set; }


        public Subscription? Subscription { get; set; }
    }


    public enum AppointmentStatus
    {
        Available,
        Pending,
        Confirmed,
        Completed
    }

    public enum AppointmentType
    {
        online,
        inclinic
    }


}
