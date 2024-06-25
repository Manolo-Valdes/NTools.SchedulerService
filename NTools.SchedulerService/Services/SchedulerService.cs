using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NTools.SchedulerService.Contracts;
using NTools.SchedulerService.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NTools.SchedulerService.Services
{
    class SchedulerService : BackgroundService
    {
        private readonly IServiceScopeFactory ScopeFactory;
        private readonly ILogger<SchedulerService> _logger;
        public SchedulerService(IServiceScopeFactory scopeFactory, ILogger<SchedulerService> logger)
        {
            ScopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var serviceScope = ScopeFactory.CreateScope())
            {
                serviceScope.ServiceProvider.ApplySchedulerMigrations();
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Loading Scheduled Tasks..");
                await ExecuteOnceAsync(cancellationToken);
                _logger.LogInformation("Waiting 5 hours until next run..");
                await Task.Delay(TimeSpan.FromMinutes(300), cancellationToken);
            }
        }

        private async Task ExecuteOnceAsync(CancellationToken cancellationToken)
        {
            var taskFactory = new TaskFactory(TaskScheduler.Current);
            var referenceTime = DateTime.UtcNow;

            using (var serviceScope = ScopeFactory.CreateScope())
            {
                IScheduledTaskFactory ScheduledTaskFactory = serviceScope.ServiceProvider.GetRequiredService<IScheduledTaskFactory>();
                if (ScheduledTaskFactory == null)
                {
                    ArgumentNullException argumentNullException = new ArgumentNullException(nameof(ScheduledTaskFactory));
                    _logger.LogError(argumentNullException.Message);
                    throw argumentNullException;
                }

                SchedulerDbContext DbContext = serviceScope.ServiceProvider.GetRequiredService<SchedulerDbContext>();

                var tasksThatShouldRun = (from task in DbContext.SchedulerTasks.AsEnumerable().Where((t) => SchedulerTaskWrapper.ShouldRun(t, referenceTime)) select new SchedulerTaskWrapper(task)).ToList();

            foreach (var taskThatShouldRun in tasksThatShouldRun)
            {
                _logger.LogInformation("Running scheduled Task..");
                taskThatShouldRun.Increment();
                await taskFactory.StartNew(
                    async () =>
                    {
                        try
                        {
                            var TaskEngine = ScheduledTaskFactory.Create(taskThatShouldRun.Task.TaskEngine);
                            await TaskEngine?.ExecuteAsync(taskThatShouldRun.Task.Id, cancellationToken);
                            await DbContext.SaveChangesAsync();
                            _logger.LogInformation("scheduled Task completed.");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                        }
                    },
                    cancellationToken);
            }
            }
        }
    }
}
