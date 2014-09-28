using System.Collections.Generic;

namespace TaskProcessor.Contracts
{
	/// <summary>
	/// A queue which a set of tasks which have to be executed.
	/// </summary>
	public interface ITaskQueue
	{
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
		/// Get all queued tasks.
		/// </summary>
		/// <returns>List of all queued tasks.<returns>
        IEnumerable<ITaskExecution> GetAll();

        /// <summary>
        /// Set a new worker status (in order to register a worker or to update
        /// a worker status after succesful processing).
        /// </summary>
        /// <param name="worker">Worker.</param>
        /// <param name="workerStatus">Worker status.</param>
        void SetWorkerStatus(IWorker worker, WorkerStatus workerStatus);

        /// <summary>
        /// Get a list of all workers.
        /// </summary>
        /// <returns>The workers.</returns>
        IEnumerable<IWorker> GetAllWorkers();

	    void Add(IEnumerable<IWorker> workers);
	    void Add(IEnumerable<ITaskExecution> tasks);
	}
}