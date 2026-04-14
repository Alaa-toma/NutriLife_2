
using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Nutrilife.DataAccessLayer.DTO.Request;
using Nutrilife.DataAccessLayer.DTO.Response;
using Nutrilife.DataAccessLayer.Models;
using Nutrilife.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nutrilife.LogicLayer.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ISubscriptionRepository _SubscriptionRepository;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser>  _UserManager;
        private readonly IMeetingLinkCreation _meetingLinkCreation;
        private readonly INutritionistRepository _NutritionistRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository,
            ISubscriptionRepository subscriptionRepository, IHttpContextAccessor httpContextAccessor, 
            IEmailSender emailSender, UserManager<ApplicationUser> UserManager, 
            IMeetingLinkCreation meetingLinkCreation,
            INutritionistRepository NutritionistRepository)
        {
            _appointmentRepository = appointmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _SubscriptionRepository = subscriptionRepository;
            _emailSender = emailSender;
            _UserManager = UserManager;
            _meetingLinkCreation = meetingLinkCreation;
            _NutritionistRepository = NutritionistRepository;
        }


        public async Task<AppointmentResponse> CreateAppointmentAsync(CreateAppointmentRequest request)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var nowTime = TimeOnly.FromDateTime(DateTime.UtcNow);


            if ( request.date < today || (request.date == today && request.Time < nowTime)  )
            {
                return new AppointmentResponse()
                {
                    Confirmd = false,
                    message = "You try to add Appointment in The Past! Check Your Appointment date"
                };
            }

            var UserId = GetCurrentUserId(); // nutri Id 
            var hasConflict = await _appointmentRepository.ISConflict(UserId, request.date, request.Time);
            if (hasConflict)
            {
                return new AppointmentResponse()
                {
                    Confirmd = false,
                    message = "Conflict Appointment! you Already an appointment for this tome.."
                };
            }

            // adapt to appointment object
            var appointment = request.Adapt<Appointment>();
            appointment.Status = AppointmentStatus.Available;

            // create and save changes
            var created = await _appointmentRepository.CreateAsync(appointment);


            return new AppointmentResponse()
            {
                Confirmd = true,
                message = "Your Appointment created Successfully..",
                AppointmentId = created.Id
            };

        }

        public async Task< List<AvailableAppointments> > GetAvailableAppointmentsAsync(int subscriptionId)
        {
            var subscribtion = await _SubscriptionRepository.GetByIdAsync(subscriptionId);
            if(subscribtion == null || subscribtion.Status != SubscriptionStatus.Active)
            {
                throw new Exception("Subscription Not Found!");
            }

            return await _appointmentRepository.AvailableAppointments(subscribtion.NutritionistId);
        }
        public async Task<AppointmentResponse> reserveAppointment(AppointmentRequest request)
        {
            var subscription = await _SubscriptionRepository.GetByIdAsync(request.SubscriptioId);
            if (subscription == null)
            {
                return new AppointmentResponse()
                {
                    Confirmd = false,
                    message = "You need to subscribe with the Nutritionist before booking an appointment."
                };
            }// client has subscription

            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
            if(appointment == null)
            {
                return new AppointmentResponse()
                {
                    Confirmd = false,
                    message = "appointment Not Found!"
                };
            }

            if (appointment.Status != AppointmentStatus.Available)
            {
                return new AppointmentResponse()
                {
                    Confirmd = false,
                    message = "appointment Not Available!"
                };
            }

            appointment.SubscriptioId = request.SubscriptioId;
            appointment.type = request.type;
            appointment.Status = AppointmentStatus.Pending;
            await _appointmentRepository.UpdateAsync(appointment);
            return new AppointmentResponse()
            {
                Confirmd = true,
                message = "Appointment Request Sent Successfully..", 
                AppointmentId = appointment.Id
            };
        }

        public async Task<List<Appointment>> GetClientAppointmentsAsync(string ClientId)
        {
            return await _appointmentRepository.GetClientAppointments(ClientId);
        }

        public async Task<List<Appointment>> GetNutritionistAppointmentsAsync(string NutriId)
        {
            return await _appointmentRepository.GetNutritionistAppointments(NutriId);
        }

        private string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<AppointmentResponse> RejectAppointmentAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return new AppointmentResponse()
                {
                    Confirmd = false,
                    message = "Appointment Not Found!"
                };
            }

            var hasSubscription = await _SubscriptionRepository.GetByIdAsync(appointment.SubscriptioId.Value);
            if (hasSubscription == null)
            {
                return new AppointmentResponse()
                {
                    Confirmd = false,
                    message = " You Should Make a Subscription First!"
                };
            }

            if (hasSubscription.NutritionistId != GetCurrentUserId())
                throw new UnauthorizedAccessException("You cannot reject this subscription");

            if (appointment.Status != AppointmentStatus.Pending)
            {
                return new AppointmentResponse()
                {
                    Confirmd = false,
                    message = "Only pending Appointments can be rejected"
                };
            }

            appointment.Status = AppointmentStatus.Available;
            var updated = await _appointmentRepository.UpdateAsync(appointment);

            return new AppointmentResponse()
            {
                Confirmd = true,
                message = "Appointment Is Rejected Succfully..",
                AppointmentId = appointmentId
            };
        }

        public async Task<AppointmentResponse> ApproveAppointmentAsync(int appointmentId)
        {
            // الموعد موجود
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return new AppointmentResponse()
                {
                    Confirmd = false,
                    message = "Appointment Not Found!"
                };
            }
            // المراجع مشترك
            var hasSubscription = await _SubscriptionRepository.GetByIdAsync(appointment.SubscriptioId.Value);
            if (hasSubscription == null)
            {
                return new AppointmentResponse()
                {
                    Confirmd = false,
                    message = " You Should Make a Subscription First!"
                };
            }
            // الاشتراك الموجود كع هذا الاخصائي
            if (hasSubscription.NutritionistId != GetCurrentUserId())
                throw new UnauthorizedAccessException("You cannot Approve this Appointment");
            //تم ارسال طلب حجز الموعد
            if (appointment.Status != AppointmentStatus.Pending)
            {
                return new AppointmentResponse()
                {
                    Confirmd = false,
                    message = "Only pending Appointments can be rejected"
                };
            }
            // قبول
            appointment.Status = AppointmentStatus.Confirmed;

            // ارسال ايميل تاكيد
            var client = await _UserManager.FindByIdAsync(hasSubscription.ClientId);

            string meetLink = "";
            if (appointment.type == AppointmentType.online)
            {
                 meetLink = await _meetingLinkCreation.CreateMeetingLinkAsync(
               title: $"Nutrition Consultation - {client.UserName}",
               start: appointment.date.ToDateTime(appointment.Time),
               end: appointment.date.ToDateTime(appointment.Time).AddMinutes(45)
           );

                await _emailSender.SendEmailAsync(
                    client.Email!,
                    "Your Appointment is Scheduled 📅",
                    $@"
                <h2>Hello {client.UserName},</h2>
                <p>Your nutrition consultation has been scheduled.</p>
                <p><strong>Date:</strong> day-{appointment.date.Day} : month-{appointment.date.Month}  year-{appointment.date.Year} </p>
                <p><strong>Time:</strong> {appointment.Time} </p>
                <br/>
                <a href='{meetLink}'
                   style='background:#1a73e8; color:white; padding:12px 24px;
                          text-decoration:none; border-radius:6px; font-size:16px;'>
                   Join Google Meet 🎥
                </a>
                <br/><br/>
                <p>Or copy this link: <a href='{meetLink}'>{meetLink}</a></p>
                "
                );
            }
            else
            {
                var nutri = await _NutritionistRepository.GetByIdAsync(hasSubscription.NutritionistId);
                await _emailSender.SendEmailAsync(
                    client.Email!,
                    "Your Appointment is Scheduled 📅",
                    $@"
                <h2>Hello {client.UserName},</h2>
                <p>Your nutrition consultation has been scheduled.</p>
                <p><strong>Date:</strong> day-{appointment.date.Day} : month-{appointment.date.Month}  year-{appointment.date.Year} </p>
                <p><strong>Time:</strong> {appointment.Time} </p>
                <br/>
                 <p><strong>Location:</strong> In {nutri.Location} clinic  </p>
                "
                );
            }
            // تعديل 
             var updated = await _appointmentRepository.UpdateAsync(appointment);
            // الرد
            return new AppointmentResponse()
            {
                Confirmd = true,
                message = "Appointment Is Approved Succfully.. ",
                AppointmentId = appointmentId,
                MeetingLink = meetLink
            };
        }

        public async Task<MessageResponse> CompleteAppointment(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return new MessageResponse() { success=false, message="Appointment Not Found." };
            }
            if(appointment.Status == AppointmentStatus.Completed)
            {
                return new MessageResponse() { success = false, message = "Already Completed!" };
            }

            appointment.Status = AppointmentStatus.Completed;
            await _appointmentRepository.UpdateAsync(appointment);
            return new MessageResponse() { success = true, message=" completed Successfuly.." };
        }


    }
}
