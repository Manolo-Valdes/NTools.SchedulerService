using NTools.SchedulerService.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTools.SchedulerServiceTests.Mock
{
    public class ScheduledTask : IScheduledTask
    {
        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("ExecuteAsync from ScheduledTask");
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(string ScheduledTaskID, CancellationToken cancellationToken)
        {
            Debug.WriteLine("ExecuteAsync from ScheduledTask " + ScheduledTaskID);
            return Task.CompletedTask;
        }
    }

    public class ScheduledTask2 : IScheduledTask
    {
        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("ExecuteAsync from ScheduledTask2");
            return Task.CompletedTask;
        }

        public Task ExecuteAsync(string ScheduledTaskID, CancellationToken cancellationToken)
        {
            Debug.WriteLine("ExecuteAsync from ScheduledTask2 " + ScheduledTaskID);
            return Task.CompletedTask;
        }
    }
}
