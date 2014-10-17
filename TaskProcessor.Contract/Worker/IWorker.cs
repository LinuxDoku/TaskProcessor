using TaskProcessor.Contract.Task;

namespace TaskProcessor.Contract.Worker
{
	/// <summary>
	/// Interface for a worker which processes tasks.
	/// </summary>
	public interface IWorker
	{
        /// <summary>
        /// Current worker status.
        /// </summary>
        WorkerStatus Status { get; }

        /// <summary>
        /// Execute a Task.
        /// </summary>
        void Execute(ITaskExecution taskExecution);

		/// <summary>
        /// Start the worker. (After it was stopped).
		/// </summary>
		void Start();

		/// <summary>
		/// Stop the worker - now!
		/// </summary>
		void Stop();

		/// <summary>
		/// Cancel the worker execution. (The worker stops in the next best moment).
		/// </summary>
		void Cancel();

		/// <summary>
		/// Abort a cancel.
		/// </summary>
		void AbortCancel();
	}
}