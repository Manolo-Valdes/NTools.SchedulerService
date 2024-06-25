namespace NTools.SchedulerService.Contracts
{
    public interface IScheduledTaskFactory
    {
        IScheduledTask Create(string ScheduledTaskName);
    }
}
