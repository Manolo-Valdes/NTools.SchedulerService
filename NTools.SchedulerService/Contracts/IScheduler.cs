using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTools.SchedulerService.Contracts
{
    public interface IScheduler
    {
        Task<string> RegisterTask(string Scheduler, string TaskEngine);
        Task<string> RegisterTask(string Scheduler, string TaskEngine, DateTime StartDate);
        void UnRegisterTask(string TaskID);
    }
}
