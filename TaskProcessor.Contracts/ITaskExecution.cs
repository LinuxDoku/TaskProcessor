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
        /// A log of the execution.
        /// </summary>
        /// <value>The log.</value>
        IEnumerable<ILog> Logs { get; }

        /// <summary>
        /// Write to log.
        /// </summary>
        /// <param name="exception"></param>
        void Log(ILog log);

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message"></param>
        void Log(string message);

        /// <summary>
        /// Write an exception to log.
        /// </summary>
        /// <param name="exception"></param>
        void Log(Exception exception);
    }
}