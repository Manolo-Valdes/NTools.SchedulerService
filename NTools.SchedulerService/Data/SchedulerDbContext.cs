using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NTools.SchedulerService.Data
{
    class SchedulerDbContext : DbContext
    {
        public SchedulerDbContext(DbContextOptions<SchedulerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Model.SchedulerTask> SchedulerTasks { get; set;}
    }
}
