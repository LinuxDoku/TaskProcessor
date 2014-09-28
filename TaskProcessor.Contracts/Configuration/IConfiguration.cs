using System.Collections.Generic;

namespace TaskProcessor.Contracts.Configuration
{
    public interface IConfiguration
    {
        /// <summary>
        /// Number of worker instances on this machine.
        /// </summary>
        /// <value>The workers.</value>
        int Workers { get; } 

        /// <summary>
        /// Tasks to execute on this machine.
        /// </summary>
        /// <value>The tasks.</value>
        IEnumerable<ITask> Tasks { get; } 
    }
}