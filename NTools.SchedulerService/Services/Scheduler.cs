using NTools.SchedulerService.Contracts;
using NTools.SchedulerService.Data;
using NTools.SchedulerService.Data.Model;
using System;
using System.Threading.Tasks;

namespace NTools.SchedulerService.Services
{
    class Scheduler : IScheduler
    {
        private readonly SchedulerDbContext DbContext;

        public Scheduler(SchedulerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task<string> RegisterTask(string Scheduler, string TaskEngine)
        {
            return RegisterTask(Scheduler, TaskEngine, DateTime.MinValue);
        }

        public async Task<string> RegisterTask(string Scheduler, string TaskEngine, DateTime StartDate)
        {
            SchedulerTask task = new SchedulerTask()
            {
                Id = Guid.NewGuid().ToString(),
                Scheduler = Scheduler,
                TaskEngine = TaskEngine,
                NextRunTime = StartDate
            };
            await DbContext.SchedulerTasks.AddAsync(task);
            await DbContext.SaveChangesAsync();
            return task.Id;
        }

        public async void UnRegisterTask(string TaskID)
        {
            var schedulerTask = await DbContext.SchedulerTasks.FindAsync(TaskID);
            if (schedulerTask != null)
            {
                DbContext.SchedulerTasks.Remove(schedulerTask);
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
