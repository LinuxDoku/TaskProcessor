using System.Collections.Generic;
using TaskProcessor.Contract.Task;
using TaskProcessor.Contract.Worker;
using TaskProcessor.Contracts;

namespace TaskProcessor.Contract.Queue {
    /// <summary>
    /// A queue which a set of tasks which have to be executed.
    /// </summary>
    public interface ITaskQueue {
        /// <summary>
        /// Identifier of the task queue.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Get all queued tasks.
        /// </summary>
        IEnumerable<ITaskExecution> Tasks { get; }

        /// <summary>
        /// Get a list of all workers.
        /// </summary>
        IEnumerable<IWorker> Workers { get; }

        /// <summary>
        /// Add a task execution to the queue.
        /// </summary>
        /// <param name="task">Task execution object.</param>
        void Add(ITaskExecution task);

        /// <summary>
        /// Add a worker.
        /// </summary>
        /// <param name="worker">Worker.</param>
        void Add(IWorker worker);

        /// <summary>
        /// Add a list of workers.
        /// </summary>
        /// <param name="workers"></param>
        void Add(IEnumerable<IWorker> workers);

        /// <summary>
        /// Add a list of task executions.
        /// </summary>
        /// <param name="tasks"></param>
        void Add(IEnumerable<ITaskExecution> tasks);

        /// <summary>
        /// Set a new worker status (in order to register a worker or to update
        /// a worker status after succesful processing).
        /// </summary>
        /// <param name="worker">Worker.</param>
        /// <param name="workerStatus">Worker status.</param>
        void SetWorkerStatus(IWorker worker, WorkerStatus workerStatus);
    }
}