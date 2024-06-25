using NTools.SchedulerService.Contracts;
using NTools.SchedulerService.Data.Model;
using System;

namespace NTools.SchedulerService
{
    class SchedulerTaskWrapper
    {
        public SchedulerTaskWrapper(SchedulerTask task)
        {
            Task = task;
            Schedule = Schedule.Parse(task.Scheduler);
        }

        public Schedule Schedule { get;}
        public SchedulerTask Task { get;}

        public void Increment()
        {
            if (Task.NextRunTime == DateTime.MinValue)
            {
                Task.NextRunTime = DateTime.Now;
            }
            Task.LastRunTime = Task.NextRunTime;
            Task.NextRunTime = Schedule.GetNextOccurrence(Task.NextRunTime);
        }

        public static bool ShouldRun(SchedulerTask Task, DateTime currentTime)
        {
            return Task.NextRunTime == DateTime.MinValue || (Task.NextRunTime < currentTime && Task.LastRunTime != Task.NextRunTime);
        }
    }
}
