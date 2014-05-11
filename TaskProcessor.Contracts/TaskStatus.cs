namespace TaskProcessor.Contracts
{
	/// <summary>
	/// Task status indicators for further processing.
	/// </summary>
	public enum TaskStatus
	{
		/// <summary>
		/// The task is newly created and not yet added to a queue.
		/// </summary>
		INITIAL,
	
		/// <summary>
		/// The task is added to a queue.
		/// </summary>
		QUEUED,

        /// <summary>
        /// Waiting for execution after removal from queue.
        /// </summary>
        WAITING,

		/// <summary>
		/// The task execution is running at the moment.
		/// </summary>
		RUNNING,

		/// <summary>
		/// The task execution failed.
		/// </summary>
		FAILED,

		/// <summary>
		/// The task was executed successful.
		/// </summary>
		SUCCESSFUL
	}
}