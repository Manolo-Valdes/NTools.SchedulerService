using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTools.SchedulerService;
using NTools.SchedulerService.Contracts;
using NTools.SchedulerService.Data;
using NTools.SchedulerService.Services;
using NTools.SchedulerServiceTests.Mock;
using System;
using System.Threading;

namespace NTools.SchedulerServiceTests.Services
{
    [TestClass()]
    public class ScheduledTaskFactoryTests
    {
        [TestMethod()]
        public void CreateTask1Test()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<IScheduledTask,ScheduledTask>();
            services.AddScoped<IScheduledTask, ScheduledTask2>();
            IServiceProvider provider = services.BuildServiceProvider();

            ScheduledTaskFactory scheduledTaskFactory = new ScheduledTaskFactory(provider);
            const string ScheduledTaskName = "NTools.SchedulerServiceTests.Mock.ScheduledTask";
            var ScheduledTask = scheduledTaskFactory.Create(ScheduledTaskName);
            Assert.IsNotNull(ScheduledTask);
            ScheduledTask.ExecuteAsync(ScheduledTaskName, new CancellationToken());

            const string ScheduledTask2Name = "NTools.SchedulerServiceTests.Mock.ScheduledTask2";
            ScheduledTask = scheduledTaskFactory.Create(ScheduledTask2Name);
            Assert.IsNotNull(ScheduledTask);
            ScheduledTask.ExecuteAsync(ScheduledTask2Name, new CancellationToken());
        }

        [TestMethod()]
        public void CreateTask2Test()
        {
            ServiceCollection services = new ServiceCollection();
            IServiceProvider provider = services.BuildServiceProvider();

            ScheduledTaskFactory scheduledTaskFactory = new ScheduledTaskFactory(provider);
            var ScheduledTask = scheduledTaskFactory.Create("NTools.SchedulerServiceTests.Mock.ScheduledTask");
            Assert.IsNull(ScheduledTask);
        }



        [TestMethod()]
        public void ServiceTest()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<IScheduledTask, ScheduledTask>();
            services.AddScoped<IScheduledTask, ScheduledTask2>();
            services.AddLogging();

            services.AddScheduler(options =>
                options.UseInMemoryDatabase(databaseName: "Add_writes_to_database"));


            IServiceProvider provider = services.BuildServiceProvider();

            var scheduler = provider.GetRequiredService<IScheduler>();

            scheduler.RegisterTask("0 0 0 1", "NTools.SchedulerServiceTests.Mock.ScheduledTask");
            scheduler.RegisterTask("0 0 1 0", "NTools.SchedulerServiceTests.Mock.ScheduledTask2");

            var ser = provider.GetRequiredService<IHostedService>();
            ser.StartAsync(new CancellationToken());
        }
    }
}
