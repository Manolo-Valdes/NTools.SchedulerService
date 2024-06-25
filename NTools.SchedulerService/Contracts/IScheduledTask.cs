using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTools.SchedulerService.Contracts
{
    public interface IScheduledTask
    {
        /// <summary>
        /// Implementa una tarea programada
        /// </summary>
        /// <param name="ScheduledTaskID">Identificador de la tarea cuando se registra en el planificador</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ExecuteAsync(string ScheduledTaskID, CancellationToken cancellationToken);
    }
}
