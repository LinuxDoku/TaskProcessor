using System;
using System.Collections.Generic;

namespace TaskProcessor.Contracts
{
    /// <summary>
    /// Protocol of a task execution.
    /// </summary>
    public interface ITaskExecution
    {
        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <value>The task.</value>
        ITask Task { get; }

        /// <summary>
        /// The current task execution status.
        /// </summary>
        /// <value>The status.</value>
        TaskStatus Status { get; set; }

        /// <summary>
        /// Console output of the task execution.
        /// </summary>
        /// <value>The console output.</value>
        string Output { get; set; }

        /// <summary>
        /// List of exceptions which occoured while the execution.
        /// </summary>
        /// <value>The exceptions.</value>
        List<Exception> Exceptions { get; set; }

        /// <summary>
        /// A log of the execution.
        /// </summary>
        /// <value>The log.</value>
        List<ILog> Log { get; }
    }
}