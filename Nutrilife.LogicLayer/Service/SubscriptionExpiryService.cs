using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nutrilife.DataAccessLayer.Data;
using Nutrilife.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilife.LogicLayer.Service
{

    // يفحص كل 24 ساعة , اذا في اشتراك حالته اكتف بس التاريخ تبعه انتهى فيحول حالته الى فنشد...
    // والاشتراك الي ينتهي بكرا نرسل ايميل تنبيه
    public class SubscriptionExpiryService : BackgroundService
    {

        private readonly IServiceScopeFactory _scopeFactory;

        public SubscriptionExpiryService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckExpiredSubscriptions();
                await CheckSubscriptionsEndingTomorrow();
                // Run every 24 hours
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

        private async Task CheckExpiredSubscriptions()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var expiredSubscriptions = await context.Subscriptions
                .Where(s => s.Status == SubscriptionStatus.Active &&
                            s.EndDate < DateOnly.FromDateTime(DateTime.UtcNow))
                .ToListAsync();

            foreach (var subscription in expiredSubscriptions)
            {
                subscription.Status = SubscriptionStatus.Finished;
            }

            if (expiredSubscriptions.Any())
                await context.SaveChangesAsync();
        }

        private async Task CheckSubscriptionsEndingTomorrow()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailSender>();

            var tomorrow = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));

            var subscriptionsEndingTomorrow = await context.Subscriptions
                .Where(s => s.Status == SubscriptionStatus.Active &&
                            s.EndDate == tomorrow
                            && s.Client != null &&
                s.Client.Email != null)
                .Include(s => s.Client)
                .ToListAsync();



            foreach (var subscription in subscriptionsEndingTomorrow)
            {
                if (string.IsNullOrWhiteSpace(subscription.Client?.Email))
                    continue;

                await emailService.SendEmailAsync(
                    subscription.Client.Email, 
                    "انتهاء الاشتراك..!",
                    $" <body style=\"\"font-family:Arial,sans-serif;direction:rtl;text-align:right;line-height:1.8;color:#333;\"\">\r\n  " +
                    $"  <div style=\"\"padding:20px;\"\">\r\n      " +
                    $"  <h2 style=\"\"color:#2c7a7b;\"\">تنبيه انتهاء الاشتراك</h2>\r\n        \r\n    " +
                    $"    <p>\r\n            مرحباً <strong>{{subscription.Client.FullName}}</strong>,\r\n       " +
                    $" </p>\r\n        \r\n        <p>\r\n            اشتراكك الحالي مع اخصائي|ة التغذية\r\n       " +
                    $"     <strong>{{subscription.Nutritionist.FullName}}</strong>\r\n            ينتهي غداً\r\n    " +
                    $"        <strong>({{subscription.EndDate}})</strong>.\r\n        </p>\r\n        \r\n     " +
                    $"   <p>\r\n            سارع بتجديده.\r\n        </p>\r\n    </div>\r\n</body> ");
            }
        }





    }
}
