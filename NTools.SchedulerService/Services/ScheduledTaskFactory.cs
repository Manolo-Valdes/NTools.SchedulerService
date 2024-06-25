using NTools.SchedulerService.Contracts;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace NTools.SchedulerService.Services
{
    public class ScheduledTaskFactory : IScheduledTaskFactory
    {
        private readonly IServiceProvider Provider;

        public ScheduledTaskFactory(IServiceProvider provider)
        {
            Provider = provider;
        }

        public IScheduledTask Create(string ScheduledTaskName)
        {
            IScheduledTask scheduledTask = Provider.GetServices<IScheduledTask>().FirstOrDefault((t) => t.GetType().FullName == ScheduledTaskName);
            return scheduledTask;
        }
    }
}
