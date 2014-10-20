namespace TaskProcessor.Contract.Task
{
	/// <summary>
	/// Task status indicators for further processing.
	/// </summary>
	public enum TaskStatus
	{
		/// <summary>
		/// The task is newly created and not yet added to a queue.
		/// </summary>
		Initial,
	
		/// <summary>
		/// The task is added to a queue.
		/// </summary>
		Queued,

        /// <summary>
        /// Waiting for execution after removal from queue.
        /// </summary>
        Waiting,

		/// <summary>
		/// The task execution is running at the moment.
		/// </summary>
		Running,

		/// <summary>
		/// The task execution failed.
		/// </summary>
		Failed,

		/// <summary>
		/// The task was executed successful.
		/// </summary>
		Successful
	}
}