//using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NTools.SchedulerService.Contracts;
using NTools.SchedulerService.Data;
using NTools.SchedulerService.Services;
using System;

namespace NTools.SchedulerService
{
    public static class Extensions
    {
        public static IServiceCollection AddScheduler(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            return services.AddScheduler<ScheduledTaskFactory>(optionsAction);
        }
        public static void ApplySchedulerMigrations(this IServiceProvider services)
        {
            services.GetService<SchedulerDbContext>().Database.Migrate() ;
        }

        public static IServiceCollection AddScheduler<TImplementation>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction) where TImplementation : class, IScheduledTaskFactory
        {
            services.AddDbContext<SchedulerDbContext>(optionsAction);
            services.AddScoped<IScheduler, Scheduler>();
            return services.AddScoped<IScheduledTaskFactory, TImplementation> ();
            // services.AddHostedService<Services.SchedulerService>();
        }

        public static IHostBuilder AddScheduler(this IHostBuilder Host)
        {
            return Host.ConfigureServices(services =>
            {
                services.AddHostedService<Services.SchedulerService>();
            });
        }


        public static IWebHostBuilder AddScheduler(this IWebHostBuilder Host)
        {
            return Host.ConfigureServices(services =>
            {
                services.AddHostedService<Services.SchedulerService>();
            });
        }

    }
}
