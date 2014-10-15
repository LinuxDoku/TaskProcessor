using System.Collections.Generic;

namespace TaskProcessor.Contracts {
    /// <summary>
    /// A queue which a set of tasks which have to be executed.
    /// </summary>
    public interface ITaskQueue {
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

        void Add(IEnumerable<IWorker> workers);
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