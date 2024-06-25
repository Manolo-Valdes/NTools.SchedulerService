using System;
using System.Collections.Generic;
using System.Text;

namespace NTools.SchedulerService.Data.Model
{
    public class SchedulerTask
    {
        public string Id { get; set; }
        public string Scheduler { get; set; }
        public string TaskEngine { get; set; }
        public DateTime LastRunTime { get; set; }
        public DateTime NextRunTime { get; set; }
    }
}
