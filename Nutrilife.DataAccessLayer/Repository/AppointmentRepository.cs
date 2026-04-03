using Microsoft.EntityFrameworkCore;
using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.DTO.Response;
using Nutrilife.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.DataAccessLayer.Repository
{
    public class AppointmentRepository :GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

       

        public async Task<List<Appointment>> GetClientAppointments(string ClientrId)
        {
            return await _context.Appointments.Where(a => a.Subscription.ClientId == ClientrId).ToListAsync();
        }
        public async Task<List<Appointment>> GetNutritionistAppointments(string NutritionistId)
        {
            return await _context.Appointments.Where(a => a.Subscription.NutritionistId == NutritionistId).ToListAsync();
        }
        public async Task<bool> ISConflict(String UserId,DateOnly date, TimeOnly time )
        {
            return await _context.Appointments.AnyAsync(a =>  (a.Status != AppointmentStatus.Available) 
            && (a.Time == time && a.date == date)
             ); // if there an conflict return true.
        }


        public async Task<Appointment?> GetByIdAsync(int AppointmentId)
        {
            return await GetOne(
                filter: s => s.Id == AppointmentId,
                includes: new[] { "Subscription", "Subscription.Client", "Subscription.Nutritionist" });
        }

        async Task<Appointment> IAppointmentRepository.UpdateAsync(Appointment appointment)
        {

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }
    }
}
